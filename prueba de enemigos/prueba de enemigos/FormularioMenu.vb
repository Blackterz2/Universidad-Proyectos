Imports System.Media
Imports AxWMPLib

Public Class FormularioMenu
    Private videoFondoMenu As String = Application.StartupPath & "\Videos\NaveReposo.mp4"
    Private videoTransicionComenzar As String = Application.StartupPath & "\Videos\NaveInicia.mp4"
    Private videoTransicionSalir As String = Application.StartupPath & "\Videos\Salir.mp4"

    ' Flag para controlar el cierre del formulario
    Private cerrandoPorAccion As Boolean = False

    ' Variable para indicar qué acción tomará el Timer (Iniciar Juego o Salir)
    Private accionPendiente As String

    Private Sub FormularioMenu_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Me.Text = "Menú Principal - Nave Espacial"

        ' --- Configuración del Windows Media Player para el video de fondo ---
        If Not System.IO.File.Exists(videoFondoMenu) Then
            MessageBox.Show("El archivo de video del menú principal no se encontró en: " & videoFondoMenu & vbCrLf & _
                            "Asegúrese de que 'NaveRepso.mp4' esté en la carpeta 'Videos' dentro de la carpeta del ejecutable.", _
                            "Error de Archivo", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            With wmpFondo                                                   ' Asegúrate de que tu control AxWindowsMediaPlayer se llame wmpFondo
                .URL = videoFondoMenu
                .settings.setMode("loop", True)                             ' Reproducción en bucle
                .settings.autoStart = True                                  ' Inicia automáticamente
                .uiMode = "none"                                            ' Oculta los controles
                .stretchToFit = True                                        ' Ajusta al tamaño del control
                .settings.volume = 0                                        ' Silencia el video de fondo del menú
                .Ctlcontrols.play()                                         ' Asegurarse de que el video empiece a reproducirse
            End With
        End If

        ' Reproducir música de fondo del menú
        If My.Resources.SueveMusic IsNot Nothing Then
            Try
                My.Computer.Audio.Play(My.Resources.SueveMusic, AudioPlayMode.BackgroundLoop)
            Catch ex As Exception
                MessageBox.Show("Error al reproducir música del menú: " & ex.Message, "Error de Audio", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("Recurso de música de menú 'MusicaMenu' no encontrado.", "Advertencia de Audio", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub FormularioMenu_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If Not cerrandoPorAccion Then
            My.Computer.Audio.Stop()
            If wmpFondo IsNot Nothing Then
                wmpFondo.Ctlcontrols.stop()
                wmpFondo.close()                                    ' Liberar recursos del WMP
            End If
            If TimerTransicion IsNot Nothing Then
                TimerTransicion.Stop()                              ' Asegurarse de detener el timer si está activo
                TimerTransicion.Dispose()                           ' Liberar el timer
            End If
        End If
    End Sub

    Private Sub BtnIniciarJuego_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnIniciarJuego.Click
        My.Computer.Audio.Stop()                                    ' Detener la música del menú
        BtnIniciarJuego.Visible = False
        BtnSalir.Visible = False
        BtnPuntajesAltos.Visible = False
        CargarYReproducirVideoTransicion(videoTransicionComenzar, 4000, "IniciarJuego")
    End Sub

    ' ---cargar y reproducir videos de transición con Timer ---
    Private Sub CargarYReproducirVideoTransicion(ByVal videoPath As String, ByVal duracionMs As Integer, ByVal proximaAccion As String)
        If wmpFondo IsNot Nothing Then
            wmpFondo.Ctlcontrols.stop()
        End If

        If Not System.IO.File.Exists(videoPath) Then
            MessageBox.Show("El archivo de video de transición no se encontró en: " & videoPath & vbCrLf & _
                            "Asegúrese de que el video esté en la carpeta 'Videos' dentro de la carpeta del ejecutable.", _
                            "Error de Archivo", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' Si el video no se encuentra, ejecuta la acción directamente
            If proximaAccion = "Salir" Then
                cerrandoPorAccion = True
                Application.Exit()
            Else ' 
                cerrandoPorAccion = True
                Me.Hide()
                Dim juegoForm As New FormularioJuego()
                juegoForm.ShowDialog()
                cerrandoPorAccion = False
                ReanudarMenu()

                Me.Show()
            End If
            Return
        End If

        If wmpFondo IsNot Nothing Then
            With wmpFondo
                .URL = videoPath
                .settings.setMode("loop", False)
                .settings.autoStart = True
                .uiMode = "none"
                .stretchToFit = True
                .settings.volume = 100
                .Ctlcontrols.play()
            End With
        End If

        Me.TimerTransicion.Interval = duracionMs                                    ' Establece la duración en milisegundos
        Me.accionPendiente = proximaAccion                                          ' Guarda la acción a realizar
        Me.TimerTransicion.Start()                                                  ' Inicia el temporizador
    End Sub

    Private Sub BtnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSalir.Click

        My.Computer.Audio.Stop()                                                    ' Detener cualquier audio antes de reproducir el video de salida
        BtnIniciarJuego.Visible = False
        BtnSalir.Visible = False
        BtnPuntajesAltos.Visible = False
        ' Configurar el video de transición y el Timer
        CargarYReproducirVideoTransicion(videoTransicionSalir, 5000, "Salir")

    End Sub

    Private Sub TimerTransicion_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerTransicion.Tick
        Me.TimerTransicion.Stop()                                                   ' Detener el Timer para que no se dispare repetidamente

        If wmpFondo IsNot Nothing Then
            wmpFondo.Ctlcontrols.stop()                                             ' Detener el video de transición
        End If

        ' Ejecutar la acción pendiente
        If accionPendiente = "Salir" Then
            cerrandoPorAccion = True
            Application.Exit()                                                      ' Cierra toda la aplicación
        ElseIf accionPendiente = "IniciarJuego" Then
            cerrandoPorAccion = True                                                ' Indicamos que estamos haciendo una acción planificada
            Me.Hide()                                                               ' Oculta el formulario de menú
            Dim juegoForm As New FormularioJuego()
            juegoForm.ShowDialog()                                                  ' Muestra el juego como un diálogo modal
            BtnIniciarJuego.Visible = True
            BtnSalir.Visible = True
            BtnPuntajesAltos.Visible = True
            cerrandoPorAccion = False                                               ' Reestablecer la bandera
            ReanudarMenu()                                                          ' Reanudar audio y video de fondo al volver
            Me.Show()                                                               ' Vuelve a mostrar el formulario de menú
        End If
    End Sub

    ' Método auxiliar para reanudar el menú (audio y video)
    Private Sub ReanudarMenu()
        My.Computer.Audio.Play(My.Resources.SueveMusic, AudioPlayMode.BackgroundLoop)
        If System.IO.File.Exists(videoFondoMenu) AndAlso wmpFondo IsNot Nothing Then
            With wmpFondo
                .URL = videoFondoMenu
                .settings.setMode("loop", True)
                .settings.autoStart = True
                .settings.volume = 0
                .Ctlcontrols.play()
            End With
        End If
    End Sub

   
    Private Sub BtnPuntajesAltos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPuntajesAltos.Click
        My.Computer.Audio.Stop()                                                                            ' Detener la música del menú temporalmente si quieres
        Dim puntajesForm As New FormularioPuntajesAltos()                                                   ' Mostrar el formulario de puntajes altos
        puntajesForm.ShowDialog()                                                                           ' Mostrarlo modalmente
        My.Computer.Audio.Play(My.Resources.SueveMusic, AudioPlayMode.BackgroundLoop)                       ' Reanudar la música del menú al volver

    End Sub
End Class