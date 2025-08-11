Imports System.Drawing
Imports System.Collections.Generic

Public Class Scroll_Galaxia

    Private Class SegmentoFondo
        Public Imagen As Image
        Public Posicion As Point
        Public Ancho As Integer ' Ancho de la imagen del segmento
        Public Alto As Integer  ' Alto de la imagen del segmento

        Public Sub New(ByVal img As Image, ByVal x As Integer, ByVal y As Integer)
            Me.Imagen = img
            Me.Posicion = New Point(x, y)
            Me.Ancho = img.Width
            Me.Alto = img.Height
        End Sub

        ' Método para mover el segmento
        Public Sub Mover(ByVal velocidadY As Integer)
            Me.Posicion = New Point(Me.Posicion.X, Me.Posicion.Y + velocidadY)
        End Sub

        ' Método para dibujar el segmento
        Public Sub Dibujar(ByVal g As Graphics)
            g.DrawImage(Me.Imagen, Me.Posicion.X, Me.Posicion.Y, Me.Ancho, Me.Alto)
        End Sub
    End Class

    ' Colección de todos los segmentos de fondo
    Private _segmentos As New List(Of SegmentoFondo)
    Public Property Speed As Point ' Velocidad de desplazamiento (Propiedad Read/Write)
    Private WidthArea As Integer   ' Ancho del área de juego
    Private HeightArea As Integer  ' Alto del área de juego

    ' Constructor o método de inicialización
    Public Sub Inicializar(ByVal ImagenesFondo As List(Of Image), ByVal Velocidad_Vertical As Integer,
                           ByVal Ancho_Pantalla As Integer, ByVal Alto_Pantalla As Integer)

        Me.Speed = New Point(0, Velocidad_Vertical) ' Solo consideramos scroll vertical en este ejemplo
        Me.WidthArea = Ancho_Pantalla
        Me.HeightArea = Alto_Pantalla

        ' Limpiar segmentos existentes si se reinicializa
        _segmentos.Clear()

        Dim currentY As Integer = 0 ' Posición Y inicial para el primer segmento (parte superior de la pantalla)
        If Speed.Y < 0 Then '
            currentY = Alto_Pantalla - ImagenesFondo(0).Height
        End If

        ' Inicializar cada segmento de fondo y añadirlos a la lista
        For Each img As Image In ImagenesFondo

            Dim segmentoHeight As Integer = img.Height ' Usa la altura real de la imagen
            If segmentoHeight <> Me.HeightArea Then
                segmentoHeight = Me.HeightArea ' Forzamos que cada segmento ocupe todo el alto del área de juego
            End If

            Dim newSegmento As New SegmentoFondo(img, 0, currentY)
            _segmentos.Add(newSegmento)

            If Speed.Y < 0 Then                                      ' Scroll hacia arriba, siguiente imagen se coloca ABAJO
                currentY += segmentoHeight                           ' El siguiente segmento empieza después del anterior
            Else                                                     ' Scroll hacia abajo, siguiente imagen se coloca ARRIBA
                currentY -= segmentoHeight                           ' El siguiente segmento empieza antes del anterior
            End If
        Next

        ' Si el scroll es hacia arriba, necesitamos que la última imagen de la secuencia
        ' esté visible primero.
        If Speed.Y < 0 Then
            Dim lastY As Integer = HeightArea ' Empieza desde la parte inferior de la pantalla
            For i As Integer = _segmentos.Count - 1 To 0 Step -1
                Dim seg As SegmentoFondo = _segmentos(i)
                seg.Posicion = New Point(0, lastY - seg.Alto)
                lastY -= seg.Alto
            Next
        End If
    End Sub

    ' LÓGICA DE LA CLASE SCROLL_GALAXIA
    Public Sub Update()
        ' Mover cada segmento y reposicionarlo si sale de la pantalla
        For Each seg As SegmentoFondo In _segmentos
            seg.Mover(Speed.Y)
            If Speed.Y > 0 Then                                                     ' Scroll hacia abajo (fondos bajan)
                If seg.Posicion.Y >= HeightArea Then
                    ' Reposicionarlo justo encima del segmento más alto (más arriba)
                    Dim segMasArriba As SegmentoFondo = _segmentos.OrderBy(Function(s) s.Posicion.Y).FirstOrDefault()
                    If segMasArriba IsNot Nothing Then
                        seg.Posicion = New Point(seg.Posicion.X, segMasArriba.Posicion.Y - seg.Alto)
                    End If
                End If
            ElseIf Speed.Y < 0 Then                                                 ' Scroll hacia arriba (fondos suben)
                ' Si el segmento sale completamente por encima de la pantalla
                If seg.Posicion.Y + seg.Alto <= 0 Then
                    ' Reposicionarlo justo debajo del segmento más bajo (más abajo)
                    Dim segMasAbajo As SegmentoFondo = _segmentos.OrderByDescending(Function(s) s.Posicion.Y).FirstOrDefault()
                    If segMasAbajo IsNot Nothing Then
                        seg.Posicion = New Point(seg.Posicion.X, segMasAbajo.Posicion.Y + segMasAbajo.Alto)
                    End If
                End If
            End If
        Next
    End Sub

    ' Pintamos las imágenes "SE HACE EN ORDEN"
    Public Sub DRAW(ByVal d As Graphics)
        For Each seg As SegmentoFondo In _segmentos
            seg.Dibujar(d)
        Next
    End Sub
End Class