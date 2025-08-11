Imports System.Drawing

Public Class TipoEnemigo
    Public Property Imagen As Image
    Public Property Salud As Integer
    Public Property Velocidad As Integer ' Velocidad de movimiento del enemigo
    Public Property VelocidadDisparo As Long ' Tiempo entre disparos (ms)

    Public Sub New(ByVal imagenEnemigo As Image, ByVal saludBase As Integer, ByVal velocidadMov As Integer, ByVal velocidadDispMs As Long)
        Me.Imagen = imagenEnemigo
        Me.Salud = saludBase
        Me.Velocidad = velocidadMov
        Me.VelocidadDisparo = velocidadDispMs
    End Sub
End Class
