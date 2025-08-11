Imports System.Drawing

' Enum para definir los tipos de Power-ups
Public Enum PowerUpTipo
    Escudo
    DisparoDoble
    ' Puedes añadir más tipos aquí en el futuro
End Enum

Public Class PowerUp
    Public Property Posicion As Point
    Public Property Imagen As Image
    Public Property Ancho As Integer
    Public Property Alto As Integer
    Public Property Tipo As PowerUpTipo ' Definiremos esta enumeración más adelante
    Public Property Velocidad As Integer = 3 ' Velocidad a la que se mueve el power-up hacia abajo

    ''' <summary>
    ''' Constructor para la clase PowerUp.
    ''' </summary>
    ''' <param name="x">Posición X inicial del power-up.</param>
    ''' <param name="y">Posición Y inicial del power-up.</param>
    ''' <param name="imagen">La imagen visual del power-up.</param>
    ''' <param name="tipo">El tipo de power-up (Escudo, DisparoDoble, etc.).</param>
    Public Sub New(ByVal x As Integer, ByVal y As Integer, ByVal imagen As Image, ByVal tipo As PowerUpTipo)
        Me.Posicion = New Point(x, y)
        Me.Imagen = imagen
        Me.Ancho = imagen.Width
        Me.Alto = imagen.Height
        Me.Tipo = tipo
    End Sub

    ''' <summary>
    ''' Actualiza la posición del power-up, moviéndolo hacia abajo.
    ''' </summary>
    Public Sub Update()
        Me.Posicion = New Point(Me.Posicion.X, Me.Posicion.Y + Me.Velocidad)
    End Sub

    ''' <summary>
    ''' Dibuja el power-up en la pantalla.
    ''' </summary>
    ''' <param name="g">El objeto Graphics para dibujar.</param>
    Public Sub Draw(ByVal g As Graphics)
        g.DrawImage(Me.Imagen, Me.Posicion.X, Me.Posicion.Y, Me.Ancho, Me.Alto)
    End Sub

    ''' <summary>
    ''' Obtiene el rectángulo de colisión del power-up.
    ''' </summary>
    Public Function GetBounds() As Rectangle
        Return New Rectangle(Me.Posicion.X, Me.Posicion.Y, Me.Ancho, Me.Alto)
    End Function
End Class
