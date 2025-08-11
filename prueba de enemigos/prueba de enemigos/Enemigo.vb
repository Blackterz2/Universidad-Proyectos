
Imports System.Drawing
Imports System.Collections.Generic

' Enumeración para los estados de los enemigos
Public Enum EstadoEnemigo
    Descendiendo
    Disparando
    Atacando
    Eliminado
End Enum

Public Class Enemigo

    Public Posicion As Point
    Public ImagenNave As Image
    Public Ancho As Integer
    Public Alto As Integer
    Public Velocidad As Integer = 3
    Public EstadoActual As EstadoEnemigo
    Private _objetivoXDisparo As Integer
    Private _objetivoYDisparo As Integer
    Private _tiempoDesdeUltimoDisparo As Long = 0

    Public Property TiempoEntreDisparosMs As Long

    Private _tiempoEnEstadoDisparando As Long = 0
    Private Const DURACION_ESTADO_DISPARANDO_MS As Long = 1000

    Public Const POSICION_Y_DISPARO As Integer = 150

    ' --- Propiedades de Vida para el Enemigo ---
    Public Property SaludMaxima As Integer
    Public Property SaludActual As Integer
    Public Property DeberiaSerEliminado As Boolean = False
    Public Property EscapoDePantalla As Boolean = False
    Public Property BalasNuevas As New List(Of Bala)()

    Public Sub New(ByVal x As Integer, ByVal y As Integer, ByVal img As Image, Optional ByVal saludInicial As Integer = 1)
        Me.Posicion = New Point(x, y)
        Me.ImagenNave = img
        Me.Ancho = img.Width
        Me.Alto = img.Height
        Me.EstadoActual = EstadoEnemigo.Descendiendo
        Me.BalasNuevas = New List(Of Bala)()        ' Inicializar la nueva lista

        Me.SaludMaxima = saludInicial               ' Asigna la salud inicial
        Me.SaludActual = saludInicial
        Me.TiempoEntreDisparosMs = 1500             ' Valor por defecto, se sobrescribirá por TipoEnemigo
    End Sub


    Public Sub Update(ByVal jugadorCentroX As Integer, ByVal jugadorCentroY As Integer, ByVal currentTime As Long, ByVal balaImagen As Image, ByVal limiteInferiorY As Integer)
        Select Case Me.EstadoActual
            Case EstadoEnemigo.Descendiendo
                Me.Posicion = New Point(Me.Posicion.X, Me.Posicion.Y + Velocidad)
                If Me.Posicion.Y >= POSICION_Y_DISPARO Then
                    Me.Posicion = New Point(Me.Posicion.X, POSICION_Y_DISPARO)
                    Me.EstadoActual = EstadoEnemigo.Disparando
                    _tiempoEnEstadoDisparando = currentTime
                    _objetivoXDisparo = jugadorCentroX
                    _objetivoYDisparo = jugadorCentroY
                End If

            Case EstadoEnemigo.Disparando
                If currentTime - _tiempoDesdeUltimoDisparo >= Me.TiempoEntreDisparosMs Then ' <--- ¡USAR LA PROPIEDAD!
                    Me.Disparar(balaImagen)
                    _tiempoDesdeUltimoDisparo = currentTime
                End If

                If currentTime - _tiempoEnEstadoDisparando >= DURACION_ESTADO_DISPARANDO_MS Then
                    Me.EstadoActual = EstadoEnemigo.Atacando
                End If

            Case EstadoEnemigo.Atacando
                Dim dirX As Integer = _objetivoXDisparo - (Me.Posicion.X + Me.Ancho \ 2)
                Dim longitudX As Double = Math.Abs(dirX)

                If longitudX > 0 Then
                    Dim velocidadXCalculada As Double = (dirX / longitudX) * Velocidad
                    Me.Posicion = New Point(CInt(Me.Posicion.X + velocidadXCalculada), Me.Posicion.Y)
                End If
                Me.Posicion = New Point(Me.Posicion.X, Me.Posicion.Y + Velocidad)

                If Me.Posicion.Y > SystemInformation.VirtualScreen.Height + Me.Alto OrElse _
                   Me.Posicion.X + Ancho < -Ancho OrElse Me.Posicion.X > SystemInformation.VirtualScreen.Width + Ancho Then
                    Me.EstadoActual = EstadoEnemigo.Eliminado
                    Me.DeberiaSerEliminado = True
                End If

                If Me.Posicion.Y > limiteInferiorY + Me.Alto Then
                    Me.EstadoActual = EstadoEnemigo.Eliminado
                    Me.DeberiaSerEliminado = True
                End If
        End Select

        ' Si la salud llega a cero, el enemigo debe ser eliminado
        If Me.SaludActual <= 0 Then
            Me.EstadoActual = EstadoEnemigo.Eliminado
            Me.DeberiaSerEliminado = True
        End If

    End Sub

    ' Dibuja la nave enemiga 
    Public Sub Draw(ByVal g As Graphics)
        If Me.EstadoActual <> EstadoEnemigo.Eliminado Then                              ' Solo dibuja si no está eliminado
            g.DrawImage(Me.ImagenNave, Me.Posicion.X, Me.Posicion.Y, Me.Ancho, Me.Alto)
            For Each bala As Bala In BalasNuevas
                bala.Draw(g)
            Next
        End If
    End Sub

    'Dispara una bala desde el enemigo en dirección al objetivo del jugador.
    Private Sub Disparar(ByVal balaImagen As Image)
        If balaImagen Is Nothing Then
            Return ' Salir del método si la imagen de la bala es Nothing
        End If
        ' ---------------------------------------------------
        Dim balaOrigenX As Integer = Me.Posicion.X + (Me.Ancho \ 2) - (balaImagen.Width \ 2)
        Dim balaOrigenY As Integer = Me.Posicion.Y + Me.Alto                                    ' Sale de la parte inferior del enemigo

        Dim dirX As Integer = _objetivoXDisparo - balaOrigenX
        Dim dirY As Integer = _objetivoYDisparo - balaOrigenY

        Dim longitud As Double = Math.Sqrt(dirX * dirX + dirY * dirY)
        Dim velocidadBala As Integer = 7

        Dim velX As Integer = 0
        Dim velY As Integer = 0

        If longitud > 0 Then
            velX = CInt((dirX / longitud) * velocidadBala)
            velY = CInt((dirY / longitud) * velocidadBala)
        Else
            velY = velocidadBala
        End If

        '  Se añade a BalasNuevas (la lista temporal del enemigo) ---
        Dim nuevaBala As New Bala(balaOrigenX, balaOrigenY, velX, velY, False, balaImagen)
        BalasNuevas.Add(nuevaBala)

       
    End Sub


    ' Obtiene el rectángulo de colisión de la nave enemiga.
    Public Function GetBounds() As Rectangle
        Return New Rectangle(Me.Posicion.X, Me.Posicion.Y, Me.Ancho, Me.Alto)
    End Function

    ' ---  Método para recibir daño (igual que el anterior) ---
    Public Sub RecibirDanio(ByVal danio As Integer)
        Me.SaludActual -= danio
        If Me.SaludActual <= 0 Then
            Me.EstadoActual = EstadoEnemigo.Eliminado
            Me.DeberiaSerEliminado = True
        End If
    End Sub

End Class