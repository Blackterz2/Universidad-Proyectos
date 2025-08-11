' Animacion.vb
Imports System.Drawing
Imports System.Collections.Generic

Public Class Animacion
    Private frames As List(Of Image)      ' Lista de imágenes (frames) de la animación
    Private currentFrameIndex As Integer  ' Índice del frame actual que se debe dibujar
    Private frameDelayMs As Integer       ' Retraso en milisegundos antes de pasar al siguiente frame
    Private lastFrameTime As Long         ' Momento (en milisegundos del juego) en que se mostró el último frame
    Private loopAnimation As Boolean      ' True si la animación se repite indefinidamente, False si se reproduce una vez
    Public HasFinished As Boolean         ' True si la animación no loopeada ha terminado de reproducirse
    ' Constructor para la clase Animacion.
    Public Sub New(ByVal animationFrames As List(Of Image), ByVal delayMs As Integer, ByVal LOOPP As Boolean)
        Me.frames = animationFrames
        Me.frameDelayMs = delayMs
        Me.loopAnimation = LOOPP
        Me.currentFrameIndex = 0
        Me.lastFrameTime = 0
        Me.HasFinished = False
    End Sub

    ' Actualiza el estado de la animación.
    Public Sub Update(ByVal currentTime As Long)
        If frames Is Nothing OrElse frames.Count = 0 OrElse HasFinished Then Return

        If currentTime - lastFrameTime >= frameDelayMs Then
            currentFrameIndex += 1
            If currentFrameIndex >= frames.Count Then
                If loopAnimation Then
                    currentFrameIndex = 0
                Else
                    currentFrameIndex = frames.Count - 1 ' Permanecer en el último frame si no es un bucle
                    HasFinished = True                   ' Marcar la animación como terminada
                End If
            End If
            lastFrameTime = currentTime
        End If
    End Sub


    ' Dibuja el frame actual de la animación en la posición y tamaño especificados.
    Public Sub Draw(ByVal g As Graphics, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer)
        If frames Is Nothing OrElse frames.Count = 0 OrElse (HasFinished AndAlso Not loopAnimation) Then Return ' No dibujar si no hay frames o si terminó y no es un bucle

        If currentFrameIndex >= 0 AndAlso currentFrameIndex < frames.Count Then
            g.DrawImage(frames(currentFrameIndex), x, y, width, height)
        End If
    End Sub

    ' Reinicia la animación al primer frame. 

    Public Sub Reset()
        Me.currentFrameIndex = 0
        Me.lastFrameTime = 0
        Me.HasFinished = False
    End Sub

    Public ReadOnly Property CurrentFrame As Image
        Get
            If frames IsNot Nothing AndAlso frames.Count > 0 AndAlso currentFrameIndex >= 0 AndAlso currentFrameIndex < frames.Count Then
                Return frames(currentFrameIndex)
            Else
                Return Nothing
            End If
        End Get
    End Property
End Class
