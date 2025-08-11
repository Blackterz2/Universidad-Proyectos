Public Class Puntuacion
    
        Public Property NombreJugador As String
        Public Property Puntos As Integer

        Public Sub New(ByVal nombre As String, ByVal puntos As Integer)
            Me.NombreJugador = nombre
            Me.Puntos = puntos
        End Sub

    ' ---Método para representar la puntuación como una cadena---
        Public Overrides Function ToString() As String
        Const MAX_NOMBRE_LENGTH As Integer = 4                      ' Máximo de letras para el nombre
        Const ANCHO_PUNTOS_FIELD As Integer = 7                     ' Ancho del campo para los puntos (ajusta según tus máximos puntos, ej. 99999 son 5 dígitos)
        Dim nombreFormateado As String = Me.NombreJugador

        ' Truncar el nombre si es más largo que MAX_NOMBRE_LENGTH
        If nombreFormateado.Length > MAX_NOMBRE_LENGTH Then
            nombreFormateado = nombreFormateado.Substring(0, MAX_NOMBRE_LENGTH)
        End If
        Return String.Format("{0,-" & MAX_NOMBRE_LENGTH & "}{1," & ANCHO_PUNTOS_FIELD & "} PUNTOS", nombreFormateado, Me.Puntos)
        End Function
    End Class

