
Imports System.Drawing


Public Class Bala
    Public Property Posicion As Point
    Public Property VelocidadX As Integer
    Public Property VelocidadY As Integer
    Public Property EsDelJugador As Boolean
    Public Property ImagenBala As Image
    Public Property Ancho As Integer
    Public Property Alto As Integer

    Public Property AnimacionBala As Animacion

    Public Sub New(ByVal x As Integer, ByVal y As Integer, ByVal velX As Integer, ByVal velY As Integer, ByVal esJugador As Boolean, ByVal imagen As Image, Optional ByVal animFrames As List(Of Image) = Nothing, Optional ByVal animFrameDuration As Long = 0) ' <-- ¡NUEVOS PARÁMETROS OPCIONALES!
        Me.Posicion = New Point(x, y)
        Me.VelocidadX = velX
        Me.VelocidadY = velY
        Me.EsDelJugador = esJugador

        If esJugador AndAlso animFrames IsNot Nothing AndAlso animFrames.Count > 0 Then
            Me.AnimacionBala = New Animacion(animFrames, animFrameDuration, True)
            Me.Ancho = animFrames(0).Width
            Me.Alto = animFrames(0).Height
            Me.ImagenBala = Nothing
        Else

            ' --- VERIFICACIÓN DE SEGURIDAD PARA LA IMAGEN ---
            Me.AnimacionBala = Nothing
            If imagen IsNot Nothing Then
                Me.ImagenBala = imagen
                Me.Ancho = imagen.Width
                Me.Alto = imagen.Height
            Else
                Me.ImagenBala = Nothing
                Me.Ancho = 20
                Me.Alto = 61
            End If
            Me.AnimacionBala = Nothing ' Asegurarse de que no haya animación para balas estáticas
        End If
    End Sub
    ' Actualiza la posición de la bala.
    Public Sub Update(Optional ByVal currentTime As Long = 0)
        Me.Posicion = New Point(Me.Posicion.X + Me.VelocidadX, Me.Posicion.Y + Me.VelocidadY)

        If Me.AnimacionBala IsNot Nothing Then
            Me.AnimacionBala.Update(currentTime) ' Actualizar la animación
        End If
    End Sub
   
    ' Dibuja la bala en la pantalla.
    Public Sub Draw(ByVal g As Graphics)
        If Me.AnimacionBala IsNot Nothing Then
            ' Si tiene animación, dibujar el frame actual de la animación
            Me.AnimacionBala.Draw(g, Me.Posicion.X, Me.Posicion.Y, Me.Ancho, Me.Alto)
        ElseIf Me.ImagenBala IsNot Nothing Then
            ' Si no tiene animación, dibujar la imagen estática (para balas enemigas)
            g.DrawImage(Me.ImagenBala, Me.Posicion.X, Me.Posicion.Y, Me.Ancho, Me.Alto)
        Else
            Dim colorBala As Color = If(Me.EsDelJugador, Color.White, Color.Red)
            g.FillRectangle(New SolidBrush(colorBala), Me.Posicion.X, Me.Posicion.Y, Me.Ancho, Me.Alto)
        End If
    End Sub
    'Obtiene el rectángulo de colisión de la bala
    Public Function GetBounds() As Rectangle
        Return New Rectangle(Me.Posicion.X, Me.Posicion.Y, Me.Ancho, Me.Alto)
    End Function
End Class
