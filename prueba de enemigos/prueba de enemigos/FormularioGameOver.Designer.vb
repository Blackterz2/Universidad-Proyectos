<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormularioGameOver
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
        Me.lblTituloGameOver = New System.Windows.Forms.Label()
        Me.BtnReitentar = New System.Windows.Forms.Button()
        Me.BtnVolverMenu = New System.Windows.Forms.Button()
        Me.btnVerPuntajes = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblPuntuacionFinal = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblTituloGameOver
        '
        Me.lblTituloGameOver.AutoSize = True
        Me.lblTituloGameOver.Font = New System.Drawing.Font("horizon", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTituloGameOver.ForeColor = System.Drawing.Color.Red
        Me.lblTituloGameOver.Location = New System.Drawing.Point(190, 149)
        Me.lblTituloGameOver.Name = "lblTituloGameOver"
        Me.lblTituloGameOver.Size = New System.Drawing.Size(156, 26)
        Me.lblTituloGameOver.TabIndex = 0
        Me.lblTituloGameOver.Text = "GAME OVER"
        '
        'BtnReitentar
        '
        Me.BtnReitentar.Font = New System.Drawing.Font("horizon", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnReitentar.Location = New System.Drawing.Point(202, 255)
        Me.BtnReitentar.Name = "BtnReitentar"
        Me.BtnReitentar.Size = New System.Drawing.Size(144, 29)
        Me.BtnReitentar.TabIndex = 1
        Me.BtnReitentar.Text = "REITENTAR"
        Me.BtnReitentar.UseVisualStyleBackColor = True
        '
        'BtnVolverMenu
        '
        Me.BtnVolverMenu.Font = New System.Drawing.Font("horizon", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnVolverMenu.Location = New System.Drawing.Point(173, 310)
        Me.BtnVolverMenu.Name = "BtnVolverMenu"
        Me.BtnVolverMenu.Size = New System.Drawing.Size(191, 31)
        Me.BtnVolverMenu.TabIndex = 2
        Me.BtnVolverMenu.Text = "VOLVER A MENU"
        Me.BtnVolverMenu.UseVisualStyleBackColor = True
        '
        'btnVerPuntajes
        '
        Me.btnVerPuntajes.Location = New System.Drawing.Point(202, 363)
        Me.btnVerPuntajes.Name = "btnVerPuntajes"
        Me.btnVerPuntajes.Size = New System.Drawing.Size(141, 23)
        Me.btnVerPuntajes.TabIndex = 3
        Me.btnVerPuntajes.Text = "VER PUNTAJES"
        Me.btnVerPuntajes.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("horizon", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(81, 121)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(156, 26)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "GAME OVER"
        '
        'lblPuntuacionFinal
        '
        Me.lblPuntuacionFinal.AutoSize = True
        Me.lblPuntuacionFinal.Location = New System.Drawing.Point(44, 23)
        Me.lblPuntuacionFinal.Name = "lblPuntuacionFinal"
        Me.lblPuntuacionFinal.Size = New System.Drawing.Size(39, 13)
        Me.lblPuntuacionFinal.TabIndex = 4
        Me.lblPuntuacionFinal.Text = "Label2"
        '
        'FormularioGameOver
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(526, 398)
        Me.Controls.Add(Me.lblPuntuacionFinal)
        Me.Controls.Add(Me.btnVerPuntajes)
        Me.Controls.Add(Me.BtnVolverMenu)
        Me.Controls.Add(Me.BtnReitentar)
        Me.Controls.Add(Me.lblTituloGameOver)
        Me.Name = "FormularioGameOver"
        Me.Text = "FormularioGameOver"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblTituloGameOver As System.Windows.Forms.Label
    Friend WithEvents BtnReitentar As System.Windows.Forms.Button
    Friend WithEvents BtnVolverMenu As System.Windows.Forms.Button
    Friend WithEvents btnVerPuntajes As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblPuntuacionFinal As System.Windows.Forms.Label
End Class
