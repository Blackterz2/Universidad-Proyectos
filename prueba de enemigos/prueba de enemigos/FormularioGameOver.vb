Imports System.Drawing
Imports System.Windows.Forms

Public Class FormularioGameOver

    ' Propiedad para recibir la puntuación final
    Public Property PuntuacionFinal As Integer

    ' Eventos personalizados para comunicar la acción seleccionada al FormularioJuego
    Public Event ReintentarJuego As EventHandler
    Public Event VolverAlMenuPrincipal As EventHandler

    Private Sub FormularioGameOver_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Me.Text = "Game Over"
        Me.StartPosition = FormStartPosition.CenterParent

        ' Configurar el título
        lblTituloGameOver.Text = "GAME OVER"
        lblTituloGameOver.ForeColor = Color.Red
        lblTituloGameOver.Font = New Font("horizon", 30, FontStyle.Bold)
        lblTituloGameOver.TextAlign = ContentAlignment.MiddleCenter

        ' Mostrar la puntuación final
        lblPuntuacionFinal.Text = "Puntuación Final: " & PuntuacionFinal.ToString()
        lblPuntuacionFinal.Font = New Font("Arial", 24, FontStyle.Bold)
        lblPuntuacionFinal.TextAlign = ContentAlignment.MiddleCenter
    End Sub

    Public Sub btnVolverMenu_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnVolverMenu.Click
        RaiseEvent VolverAlMenuPrincipal(Me, EventArgs.Empty)
        Me.Close()
    End Sub

    Private Sub btnVerPuntajes_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnVerPuntajes.Click
        ' Siempre crea una NUEVA instancia del formulario de puntajes altos
        Dim puntajesForm As New FormularioPuntajesAltos()
        puntajesForm.ShowDialog() ' Lo muestra de forma modal

        MessageBox.Show("Funcionalidad de Puntajes Altos aún no implementada.", "Puntajes Altos", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub FormularioGameOver_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Detener cualquier sonido de Game Over si lo reprodujiste
        ' My.Computer.Audio.Stop()
    End Sub

    Private Sub BtnReitentar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReitentar.Click
        RaiseEvent ReintentarJuego(Me, EventArgs.Empty)
        Me.Close()
    End Sub
End Class
