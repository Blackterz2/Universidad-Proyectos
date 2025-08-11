<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormularioPuntajesAltos
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblTituloPuntajes = New System.Windows.Forms.Label()
        Me.lbPuntajes = New System.Windows.Forms.ListBox()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblTituloPuntajes
        '
        Me.lblTituloPuntajes.AutoSize = True
        Me.lblTituloPuntajes.Font = New System.Drawing.Font("horizon", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTituloPuntajes.Location = New System.Drawing.Point(24, 21)
        Me.lblTituloPuntajes.Name = "lblTituloPuntajes"
        Me.lblTituloPuntajes.Size = New System.Drawing.Size(185, 23)
        Me.lblTituloPuntajes.TabIndex = 0
        Me.lblTituloPuntajes.Text = "PUNTAJES ALTOS"
        '
        'lbPuntajes
        '
        Me.lbPuntajes.Font = New System.Drawing.Font("horizon", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbPuntajes.FormattingEnabled = True
        Me.lbPuntajes.ItemHeight = 16
        Me.lbPuntajes.Location = New System.Drawing.Point(28, 60)
        Me.lbPuntajes.Name = "lbPuntajes"
        Me.lbPuntajes.Size = New System.Drawing.Size(340, 148)
        Me.lbPuntajes.TabIndex = 1
        '
        'btnCerrar
        '
        Me.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCerrar.Font = New System.Drawing.Font("horizon", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCerrar.Location = New System.Drawing.Point(28, 344)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(97, 29)
        Me.btnCerrar.TabIndex = 2
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'FormularioPuntajesAltos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.prueba_de_enemigos.My.Resources.Resources.stars44
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(589, 385)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.lbPuntajes)
        Me.Controls.Add(Me.lblTituloPuntajes)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FormularioPuntajesAltos"
        Me.Text = "FormularioPuntajesAltos"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblTituloPuntajes As System.Windows.Forms.Label
    Friend WithEvents lbPuntajes As System.Windows.Forms.ListBox
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
End Class
