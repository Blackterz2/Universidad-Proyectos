' FormularioPuntajesAltos.vb
Imports System.Windows.Forms
Imports System.Collections.Generic

Public Class FormularioPuntajesAltos

    Private Sub FormularioPuntajesAltos_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Me.Text = "Puntajes Altos"
        Me.StartPosition = FormStartPosition.CenterParent
        CargarYMostrarPuntajes()
    End Sub

    Private Sub CargarYMostrarPuntajes()
        lbPuntajes.Items.Clear()
        Dim puntajes As List(Of Puntuacion) = GestorPuntajes.CargarPuntajes()
        If puntajes.Count > 0 Then
            Dim rank As Integer = 1
            For Each p As Puntuacion In puntajes
                lbPuntajes.Items.Add(String.Format("{0}. {1}", rank, p.ToString()))
                rank += 1
            Next
        Else
            lbPuntajes.Items.Add("No hay puntajes altos registrados aún.")
        End If
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub
End Class