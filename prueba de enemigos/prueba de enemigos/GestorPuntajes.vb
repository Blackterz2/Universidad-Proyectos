Imports System.IO                                       ' Para trabajar con archivos
Imports System.Collections.Generic                      ' Para List(Of T)
Imports System.Linq                                     ' Para ordenar la lista (OrderByDescending, Take)

Public Class GestorPuntajes
    Private Const ARCHIVO_PUNTAJES As String = "puntajes_altos.txt"
    Private Const MAX_PUNTAJES As Integer = 10          ' Queremos guardar los 10 mejores puntajes

    Public Shared Function CargarPuntajes() As List(Of Puntuacion)
        Dim puntajes As New List(Of Puntuacion)()

        If File.Exists(ARCHIVO_PUNTAJES) Then
            Try
                For Each line As String In File.ReadAllLines(ARCHIVO_PUNTAJES)
                    Dim partes As String() = line.Split(":"c) ' Dividir por el carácter ':'
                    If partes.Length = 2 Then
                        Dim nombre As String = partes(0).Trim()
                        Dim puntos As Integer
                        If Integer.TryParse(partes(1).Trim(), puntos) Then
                            puntajes.Add(New Puntuacion(nombre, puntos))
                        End If
                    End If
                Next
            Catch ex As Exception
                ' Manejar errores de lectura de archivo
                MessageBox.Show(String.Format("Error al guardar puntajes: {0}", ex.Message), "Error de Archivo", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

        ' Ordenar los puntajes de forma descendente y tomar solo los MAX_PUNTAJES
        Return puntajes.OrderByDescending(Function(p) p.Puntos).Take(MAX_PUNTAJES).ToList()
    End Function

    Public Shared Sub GuardarNuevoPuntaje(ByVal nuevaPuntuacion As Puntuacion)
        Dim puntajesActuales As List(Of Puntuacion) = CargarPuntajes()

        ' Añadir la nueva puntuación
        puntajesActuales.Add(nuevaPuntuacion)

        ' Ordenar de nuevo y tomar solo los MAX_PUNTAJES
        Dim puntajesAGuardar As List(Of Puntuacion) = puntajesActuales.OrderByDescending(Function(p) p.Puntos).Take(MAX_PUNTAJES).ToList()

        Try
            Using sw As New StreamWriter(ARCHIVO_PUNTAJES, False) ' False para sobrescribir el archivo
                For Each p As Puntuacion In puntajesAGuardar
                    sw.WriteLine(String.Format("{0}: {1}", p.NombreJugador, p.Puntos))
                Next
            End Using
        Catch ex As Exception

            MessageBox.Show(String.Format("Error al guardar puntajes: {0}", ex.Message), "Error de Archivo", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Public Shared Function EsPuntajeAlto(ByVal puntos As Integer) As Boolean
        Dim puntajesActuales As List(Of Puntuacion) = CargarPuntajes()
        If puntajesActuales.Count < MAX_PUNTAJES Then
            Return True                                 ' Hay espacio en la tabla de puntajes
        End If

        Return puntos > puntajesActuales.Last().Puntos ' es el más bajo porque está ordenado descendentemente
    End Function

End Class
