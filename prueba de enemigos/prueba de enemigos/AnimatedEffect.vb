
Imports System.Drawing

Public Class AnimatedEffect
    Public Animation As Animacion  ' La instancia de la animación a reproducir
    Public Position As Point       ' La posición (X, Y) donde se dibuja la animación
    Public Width As Integer        ' El ancho con el que se dibuja la animación
    Public Height As Integer       ' La altura con la que se dibuja la animación


    ' Constructor para un efecto animado que se reproduce en una posición específica.
    Public Sub New(ByVal anim As Animacion, ByVal x As Integer, ByVal y As Integer, ByVal w As Integer, ByVal h As Integer)
        Me.Animation = anim
        Me.Position = New Point(x, y)
        Me.Width = w
        Me.Height = h
    End Sub
End Class
