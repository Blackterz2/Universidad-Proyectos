<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormularioMenu
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormularioMenu))
        Me.wmpFondo = New AxWMPLib.AxWindowsMediaPlayer()
        Me.TimerTransicion = New System.Windows.Forms.Timer(Me.components)
        Me.BtnSalir = New System.Windows.Forms.Button()
        Me.BtnIniciarJuego = New System.Windows.Forms.Button()
        Me.PanelFondoMenu = New System.Windows.Forms.PictureBox()
        Me.BtnPuntajesAltos = New System.Windows.Forms.Button()
        CType(Me.wmpFondo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelFondoMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'wmpFondo
        '
        Me.wmpFondo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wmpFondo.Enabled = True
        Me.wmpFondo.Location = New System.Drawing.Point(0, 0)
        Me.wmpFondo.Name = "wmpFondo"
        Me.wmpFondo.OcxState = CType(resources.GetObject("wmpFondo.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wmpFondo.Size = New System.Drawing.Size(802, 650)
        Me.wmpFondo.TabIndex = 3
        '
        'TimerTransicion
        '
        '
        'BtnSalir
        '
        Me.BtnSalir.BackgroundImage = Global.prueba_de_enemigos.My.Resources.Resources.abandonar
        Me.BtnSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnSalir.Location = New System.Drawing.Point(12, 568)
        Me.BtnSalir.Name = "BtnSalir"
        Me.BtnSalir.Size = New System.Drawing.Size(127, 31)
        Me.BtnSalir.TabIndex = 2
        Me.BtnSalir.UseVisualStyleBackColor = True
        '
        'BtnIniciarJuego
        '
        Me.BtnIniciarJuego.BackgroundImage = Global.prueba_de_enemigos.My.Resources.Resources.Comenzar_partida
        Me.BtnIniciarJuego.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnIniciarJuego.FlatAppearance.BorderSize = 0
        Me.BtnIniciarJuego.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnIniciarJuego.Location = New System.Drawing.Point(12, 502)
        Me.BtnIniciarJuego.Name = "BtnIniciarJuego"
        Me.BtnIniciarJuego.Size = New System.Drawing.Size(191, 28)
        Me.BtnIniciarJuego.TabIndex = 0
        Me.BtnIniciarJuego.UseVisualStyleBackColor = True
        '
        'PanelFondoMenu
        '
        Me.PanelFondoMenu.Location = New System.Drawing.Point(0, 0)
        Me.PanelFondoMenu.Name = "PanelFondoMenu"
        Me.PanelFondoMenu.Size = New System.Drawing.Size(802, 650)
        Me.PanelFondoMenu.TabIndex = 1
        Me.PanelFondoMenu.TabStop = False
        '
        'BtnPuntajesAltos
        '
        Me.BtnPuntajesAltos.BackColor = System.Drawing.Color.White
        Me.BtnPuntajesAltos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnPuntajesAltos.FlatAppearance.BorderSize = 0
        Me.BtnPuntajesAltos.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPuntajesAltos.Font = New System.Drawing.Font("horizon", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPuntajesAltos.Location = New System.Drawing.Point(12, 536)
        Me.BtnPuntajesAltos.Name = "BtnPuntajesAltos"
        Me.BtnPuntajesAltos.Size = New System.Drawing.Size(191, 28)
        Me.BtnPuntajesAltos.TabIndex = 4
        Me.BtnPuntajesAltos.Text = "Puntajes Altos"
        Me.BtnPuntajesAltos.UseVisualStyleBackColor = False
        '
        'FormularioMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(802, 650)
        Me.Controls.Add(Me.BtnPuntajesAltos)
        Me.Controls.Add(Me.BtnSalir)
        Me.Controls.Add(Me.BtnIniciarJuego)
        Me.Controls.Add(Me.wmpFondo)
        Me.Controls.Add(Me.PanelFondoMenu)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "FormularioMenu"
        Me.Text = "FormularioMenu"
        CType(Me.wmpFondo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelFondoMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnIniciarJuego As System.Windows.Forms.Button
    Friend WithEvents PanelFondoMenu As System.Windows.Forms.PictureBox
    Friend WithEvents BtnSalir As System.Windows.Forms.Button
    Friend WithEvents wmpFondo As AxWMPLib.AxWindowsMediaPlayer
    Friend WithEvents TimerTransicion As System.Windows.Forms.Timer
    Friend WithEvents BtnPuntajesAltos As System.Windows.Forms.Button
End Class
