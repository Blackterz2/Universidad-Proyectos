<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormularioJuego
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormularioJuego))
        Me.TmrGameLoop = New System.Windows.Forms.Timer(Me.components)
        Me.pbAreaJuego = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lblContadorTicks = New System.Windows.Forms.Label()
        Me.lblVidas = New System.Windows.Forms.Label()
        Me.lblPuntuacion = New System.Windows.Forms.Label()
        Me.lblMultiplicador = New System.Windows.Forms.Label()
        Me.pnlPausa = New System.Windows.Forms.Panel()
        Me.btnPausaVolverMenu = New System.Windows.Forms.Button()
        Me.btnReanudar = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.pbAreaJuego, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPausa.SuspendLayout()
        Me.SuspendLayout()
        '
        'TmrGameLoop
        '
        Me.TmrGameLoop.Interval = 20
        '
        'pbAreaJuego
        '
        Me.pbAreaJuego.BackColor = System.Drawing.Color.Black
        Me.pbAreaJuego.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbAreaJuego.Location = New System.Drawing.Point(0, 0)
        Me.pbAreaJuego.Name = "pbAreaJuego"
        Me.pbAreaJuego.Size = New System.Drawing.Size(692, 750)
        Me.pbAreaJuego.TabIndex = 0
        Me.pbAreaJuego.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(614, 751)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'lblContadorTicks
        '
        Me.lblContadorTicks.AutoSize = True
        Me.lblContadorTicks.BackColor = System.Drawing.Color.White
        Me.lblContadorTicks.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblContadorTicks.Font = New System.Drawing.Font("horizon", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContadorTicks.Location = New System.Drawing.Point(12, 9)
        Me.lblContadorTicks.Name = "lblContadorTicks"
        Me.lblContadorTicks.Size = New System.Drawing.Size(74, 19)
        Me.lblContadorTicks.TabIndex = 1
        Me.lblContadorTicks.Text = "0              "
        '
        'lblVidas
        '
        Me.lblVidas.AutoSize = True
        Me.lblVidas.Font = New System.Drawing.Font("horizon", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVidas.ForeColor = System.Drawing.Color.White
        Me.lblVidas.Location = New System.Drawing.Point(698, 26)
        Me.lblVidas.Name = "lblVidas"
        Me.lblVidas.Size = New System.Drawing.Size(75, 17)
        Me.lblVidas.TabIndex = 3
        Me.lblVidas.Text = "Vidas: 3"
        '
        'lblPuntuacion
        '
        Me.lblPuntuacion.AutoSize = True
        Me.lblPuntuacion.Font = New System.Drawing.Font("horizon", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPuntuacion.ForeColor = System.Drawing.Color.White
        Me.lblPuntuacion.Location = New System.Drawing.Point(834, 26)
        Me.lblPuntuacion.Name = "lblPuntuacion"
        Me.lblPuntuacion.Size = New System.Drawing.Size(65, 17)
        Me.lblPuntuacion.TabIndex = 4
        Me.lblPuntuacion.Text = "Label1"
        '
        'lblMultiplicador
        '
        Me.lblMultiplicador.AutoSize = True
        Me.lblMultiplicador.Font = New System.Drawing.Font("horizon", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMultiplicador.ForeColor = System.Drawing.Color.White
        Me.lblMultiplicador.Location = New System.Drawing.Point(834, 54)
        Me.lblMultiplicador.Name = "lblMultiplicador"
        Me.lblMultiplicador.Size = New System.Drawing.Size(14, 17)
        Me.lblMultiplicador.TabIndex = 5
        Me.lblMultiplicador.Text = " "
        '
        'pnlPausa
        '
        Me.pnlPausa.BackColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlPausa.Controls.Add(Me.btnPausaVolverMenu)
        Me.pnlPausa.Controls.Add(Me.btnReanudar)
        Me.pnlPausa.Controls.Add(Me.Label1)
        Me.pnlPausa.Location = New System.Drawing.Point(333, 273)
        Me.pnlPausa.Name = "pnlPausa"
        Me.pnlPausa.Size = New System.Drawing.Size(400, 200)
        Me.pnlPausa.TabIndex = 6
        Me.pnlPausa.Visible = False
        '
        'btnPausaVolverMenu
        '
        Me.btnPausaVolverMenu.BackColor = System.Drawing.Color.Black
        Me.btnPausaVolverMenu.FlatAppearance.BorderSize = 0
        Me.btnPausaVolverMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPausaVolverMenu.Font = New System.Drawing.Font("horizon", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPausaVolverMenu.ForeColor = System.Drawing.Color.White
        Me.btnPausaVolverMenu.Location = New System.Drawing.Point(207, 116)
        Me.btnPausaVolverMenu.Name = "btnPausaVolverMenu"
        Me.btnPausaVolverMenu.Size = New System.Drawing.Size(159, 29)
        Me.btnPausaVolverMenu.TabIndex = 2
        Me.btnPausaVolverMenu.Text = "VOLVER AL MENÚ"
        Me.btnPausaVolverMenu.UseVisualStyleBackColor = False
        '
        'btnReanudar
        '
        Me.btnReanudar.BackColor = System.Drawing.Color.Black
        Me.btnReanudar.FlatAppearance.BorderSize = 0
        Me.btnReanudar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReanudar.Font = New System.Drawing.Font("horizon", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReanudar.ForeColor = System.Drawing.Color.White
        Me.btnReanudar.Location = New System.Drawing.Point(31, 116)
        Me.btnReanudar.Name = "btnReanudar"
        Me.btnReanudar.Size = New System.Drawing.Size(131, 29)
        Me.btnReanudar.TabIndex = 1
        Me.btnReanudar.Text = "REANUDAR"
        Me.btnReanudar.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("horizon", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(103, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(216, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "JUEGO EN PAUS"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FormularioJuego
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1109, 757)
        Me.Controls.Add(Me.pnlPausa)
        Me.Controls.Add(Me.lblMultiplicador)
        Me.Controls.Add(Me.lblPuntuacion)
        Me.Controls.Add(Me.lblVidas)
        Me.Controls.Add(Me.lblContadorTicks)
        Me.Controls.Add(Me.pbAreaJuego)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "FormularioJuego"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Juego de Naves"
        CType(Me.pbAreaJuego, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPausa.ResumeLayout(False)
        Me.pnlPausa.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pbAreaJuego As System.Windows.Forms.PictureBox
    Friend WithEvents TmrGameLoop As System.Windows.Forms.Timer
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblContadorTicks As System.Windows.Forms.Label
    Friend WithEvents lblVidas As System.Windows.Forms.Label
    Friend WithEvents lblPuntuacion As System.Windows.Forms.Label
    Friend WithEvents lblMultiplicador As System.Windows.Forms.Label
    Friend WithEvents pnlPausa As System.Windows.Forms.Panel
    Friend WithEvents btnPausaVolverMenu As System.Windows.Forms.Button
    Friend WithEvents btnReanudar As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
