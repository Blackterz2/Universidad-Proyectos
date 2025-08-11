

Imports System.Drawing
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Diagnostics

Public Class FormularioJuego


    Private FondoStars As Image
    Private FondoStarsList As New List(Of Image)                            'Para guardar las multiples imagenes de fondo
    Private FondoD1 As Scroll_Galaxia                                       ' Declaración del objeto Scroll_Galaxia
    Private Const AnchoFondo As Integer = 692                               ' Usar el ancho del formulario o PictureBox
    Private Const AltoFondo As Integer = 750                                ' Usar el alto del formulario o PictureBox

    ' PAUSE
    Private JuegoEnCurso As Boolean = False
    Private IsPaused As Boolean = False                                     ' Bandera para saber si el juego está pausado

    ' Objetos del juego
    Private jugador As Jugador
    Private enemigosActivos As New List(Of Enemigo)
    Private enemigosEnEspera As New Queue(Of Enemigo)
    Private balasEnemigasActivas As New List(Of Bala)


    ' Imágenes de las balas
    Private imagenBalaJugador As Image
    Private imagenBalaEnemigo As Image

    ' Imágenes de las naves enemigas para el Spawning
    Private imagenesEnemigos As New List(Of Image)
    Private listaTiposEnemigos As New List(Of TipoEnemigo)                   ' Lista para almacenar los diferentes tipos de enemigos

    ' --- RECURSOS PARA ANIMACIONES  ---
    Private explosionFrames As New List(Of Image)                            ' Frames de la animación de explosión
    Private propulsionFrames As New List(Of Image)                           ' Frames de la animación de propulsión
    Private balaJugadorAnimacionFrames As New List(Of Image)                 ' Frames de la animación de la bala aliada


    Private activeExplosions As New List(Of AnimatedEffect)                  ' Lista de explosiones que se están reproduciendo


    Private _gameTime As Long = 0                                            ' Tiempo total del juego en milisegundos

    '-- Variables para el movimiento del jugador por teclado --
    Private _moverDerecha As Boolean = False
    Private _moverIzquierda As Boolean = False


    '
    Private _maxEnemigosInicial As Integer = 2                               ' Valor inicial del máximo de enemigos en pantalla
    Private _maxEnemigosActual As Integer                                    ' Máximo de enemigos visibles a la vez (dinámico)
    Private Const MAX_ENEMIGOS_FINAL As Integer = 8                          ' Límite máximo absoluto de enemigos en pantalla
    Private Const INCREMENTO_MAX_ENEMIGOS As Integer = 1                     ' Cuánto aumenta el máximo de enemigos
    Private Const TIEMPO_PARA_INCREMENTO_MAX_ENEMIGOS_MS As Long = 20000     ' Cada cuánto tiempo aumenta el máximo de enemigos (20 segundos)
    Private _tiempoUltimoIncrementoMaxEnemigos As Long = 0


    Private _tiempoUltimoSpawn As Long = 0
    Private _tiempoEntreSpawnActual As Long = 3000                           ' Tiempo inicial entre spawns

    Private rnd As New Random()                                              ' Para posiciones aleatorias

    Private _contadorTicks As Long = 0                                       ' VARIABLE PARA EL CONTADOR DE TICKS ---

    ' --- VARIABLES PARA LA DIFICULTAD PROGRESIVA ---
    Private _velocidadFondoInicial As Integer = 1                            ' Velocidad inicial del fondo (pixels por tick)
    Private _velocidadFondoActual As Integer                                 ' Velocidad actual del fondo

    Private Const INCREMENTO_VELOCIDAD_FONDO As Integer = 1                  ' Cuánto aumenta la velocidad del fondo
    Private Const TIEMPO_PARA_INCREMENTO_VELOCIDAD_MS As Long = 10000        ' Cada cuánto tiempo aumenta la velocidad (10 segundos)
    Private _tiempoUltimoIncrementoVelocidad As Long = 0

    Private Const REDUCCION_TIEMPO_SPAWN_MS As Long = 200                    ' Cuánto se reduce el tiempo entre spawns
    Private Const TIEMPO_PARA_REDUCIR_SPAWN_MS As Long = 15000               ' Cada cuánto tiempo se reduce el tiempo de spawn (15 segundos)
    Private Const TIEMPO_SPAWN_MINIMO_MS As Long = 500                       ' Tiempo mínimo entre spawns para que no sea inmanejable
    Private _tiempoUltimaReduccionSpawn As Long = 0

    ' --- VARIABLES PARA VIDAS Y PUNTUACIÓN ---
    Private PuntuacionActual As Integer = 0
    Private MultiplicadorPuntuacion As Integer = 1
    Private Const MAX_MULTIPLICADOR As Integer = 10
    Private Const VALOR_BASE_ENEMIGO As Integer = 10                         ' Puntos base por eliminar un enemigo

    ' --- VARIABLES PARA POWER-UPS ---
    Private activePowerUps As New List(Of PowerUp)                           ' Lista de power-ups activos en pantalla
    Private imagenEscudo As Image
    Private imagenDobleDisparo As Image

    Private _tiempoUltimoPowerUpSpawn As Long = 0
    Private Const TIEMPO_ENTRE_POWERUP_SPAWN_MS As Long = 15000              ' Cada cuánto aparece un power-up (15 segundos)
    Private Const PROBABILIDAD_POWERUP As Integer = 10                       ' De 1 a 10 (ej: 3 significa 30% de probabilidad de que aparezca un power-up)

    Private IsInDesignMode As Boolean                                        ' Para determinar si el formulario se está ejecutando en el entorno


    '----FORMULARIO DEL JUEGO LOAD

    Private Sub FormularioJuego_Load_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        IsInDesignMode = Me.DesignMode OrElse System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Designtime

        If IsInDesignMode Then
            Return ' No ejecutar lógica de juego en tiempo de diseño
        End If

        ' --- Reproducir música de fondo del juego ---
        If My.Resources.MusicRock IsNot Nothing Then
            Try
                My.Computer.Audio.Play(My.Resources.MusicRock, AudioPlayMode.BackgroundLoop)
            Catch ex As Exception
                MessageBox.Show("Error al reproducir música del juego: " & ex.Message, "Error de Audio", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("Recurso de música de juego 'MusicaJuego' no encontrado.", "Advertencia de Audio", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        ' Cargar los fondos 
        If My.Resources.panorama1 IsNot Nothing Then
            FondoStarsList.Add(My.Resources.panorama1)
        End If
        If My.Resources.panorama2 IsNot Nothing Then
            FondoStarsList.Add(My.Resources.panorama2)
        End If
        If My.Resources.panorama3 IsNot Nothing Then
            FondoStarsList.Add(My.Resources.panorama3)
        End If

        If My.Resources.panorama4 IsNot Nothing Then FondoStarsList.Add(My.Resources.panorama4)
        If My.Resources.panorama5 IsNot Nothing Then FondoStarsList.Add(My.Resources.panorama5)

        If FondoStarsList.Count > 0 Then
            FondoD1 = New Scroll_Galaxia()
            _velocidadFondoActual = _velocidadFondoInicial ' Inicializar la velocidad actual del fondo 
            FondoD1.Inicializar(FondoStarsList, _velocidadFondoActual, AnchoFondo, AltoFondo)
            Me.BackColor = Color.Black
        Else
            MessageBox.Show("No se encontraron recursos de imagen para el fondo de la galaxia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Return
        End If

        If Me.Controls.ContainsKey("lblContadorTicks") Then
            Me.lblContadorTicks.Text = "Ticks: 0"
        End If

        ' Cargar imágenes de naves y balas
        If My.Resources.navealiada8 IsNot Nothing AndAlso _
           My.Resources.NaveEnemiga83 IsNot Nothing AndAlso _
           My.Resources.naveEneimga82 IsNot Nothing AndAlso _
           My.Resources.Naveenimga84 IsNot Nothing Then

            ' Naves enemigas para el pool
            imagenesEnemigos.Add(My.Resources.NaveEnemiga83) ' Enemigo 1 (normal)
            imagenesEnemigos.Add(My.Resources.naveEneimga82) ' Enemigo 2 (rápido)
            imagenesEnemigos.Add(My.Resources.Naveenimga84) ' Enemigo 3 (tanque)



            '  Cargar y division de la imagen de la explosión ---
            If My.Resources.Explosion1 IsNot Nothing Then
                Dim fullSpriteSheet As Image = My.Resources.Explosion1

                ' Division de la imagen
                Dim numFramesX As Integer = 10
                Dim frameWidth As Integer = fullSpriteSheet.Width \ numFramesX
                Dim frameHeight As Integer = fullSpriteSheet.Height

                explosionFrames.Clear()                                                         ' Limpiar la lista antes de rellenar
                For i As Integer = 0 To numFramesX - 1
                    Dim frameRect As New Rectangle(i * frameWidth, 0, frameWidth, frameHeight)
                    Dim frame As Image = CType(fullSpriteSheet, Bitmap).Clone(frameRect, fullSpriteSheet.PixelFormat)
                    explosionFrames.Add(frame)
                Next

                If explosionFrames.Count = 0 Then
                    MessageBox.Show("Advertencia: Fallo al cargar o dividir el spritesheet de explosión.", "Recursos faltantes", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Else
                MessageBox.Show("Advertencia: El spritesheet 'ExplosionSpriteSheet' no se encontró en los recursos. Las explosiones no se mostrarán.", "Recursos faltantes", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
           
            If My.Resources.Propulsion_FrameNuevo1 IsNot Nothing Then propulsionFrames.Add(My.Resources.Propulsion_FrameNuevo1)
            If My.Resources.Propulsion_FrameNuevo2 IsNot Nothing Then propulsionFrames.Add(My.Resources.Propulsion_FrameNuevo2)
            If propulsionFrames.Count = 0 Then
                MessageBox.Show("Advertencia: No se encontraron los frames de la animación de propulsión. La propulsión del jugador no se mostrará.", "Recursos faltantes", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

            ' ---  Agregamoos los diferentes tipos de enemigos a la lista ---
            listaTiposEnemigos.Add(New TipoEnemigo(My.Resources.NaveEnemiga83, saludBase:=1, velocidadMov:=3, velocidadDispMs:=1500))
            listaTiposEnemigos.Add(New TipoEnemigo(My.Resources.naveEneimga82, saludBase:=1, velocidadMov:=5, velocidadDispMs:=1000))
            listaTiposEnemigos.Add(New TipoEnemigo(My.Resources.Naveenimga84, saludBase:=3, velocidadMov:=2, velocidadDispMs:=2000))

            '  Cargar y dividir el spritesheet de la animación de la bala del jugador ---
            If My.Resources.NewBala IsNot Nothing Then
                Dim fullBulletSpriteSheet As Image = My.Resources.NewBala
                Dim numFramesX As Integer = 6
                Dim frameWidthBala As Integer = fullBulletSpriteSheet.Width \ numFramesX             ' Ancho de un solo frame
                Dim frameHeightBala As Integer = fullBulletSpriteSheet.Height                        ' La altura es la misma que la del spritesheet completo

                balaJugadorAnimacionFrames.Clear()
                '--- Realizacion de Area de colision y corte de la animacion---
                For i As Integer = 0 To numFramesX - 1
                    Dim frameRect As New Rectangle(i * frameWidthBala, 0, frameWidthBala, frameHeightBala)
                    Dim frame As Image = CType(fullBulletSpriteSheet, Bitmap).Clone(frameRect, fullBulletSpriteSheet.PixelFormat)
                    balaJugadorAnimacionFrames.Add(frame)
                Next

                If balaJugadorAnimacionFrames.Count = 0 Then
                    MessageBox.Show("Advertencia: Fallo al cargar o dividir el spritesheet de la nueva bala del jugador.", "Recursos faltantes", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Else
                MessageBox.Show("Advertencia: El spritesheet 'AnimacionBalaVerde' de la bala del jugador no se encontró.", "Recursos faltantes", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If



            ' --- CARGA DE  IMAGENES DE POWER UP---
            If My.Resources.escudo1 IsNot Nothing Then ' Asume que tienes este recurso
                imagenEscudo = My.Resources.escudo1
            Else
                MessageBox.Show("Advertencia: Imagen de Escudo no encontrada.", "Recursos Faltantes", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                imagenEscudo = New Bitmap(50, 50)
                Using g As Graphics = Graphics.FromImage(imagenEscudo)
                    g.FillEllipse(Brushes.Blue, 0, 0, 50, 50)
                End Using
            End If
            If My.Resources.baladoble_removebg_preview IsNot Nothing Then
                imagenDobleDisparo = My.Resources.baladoble_removebg_preview
            Else
                MessageBox.Show("Advertencia: Imagen de Doble Disparo no encontrada.", "Recursos Faltantes", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                imagenDobleDisparo = New Bitmap(40, 40)
                Using g As Graphics = Graphics.FromImage(imagenDobleDisparo)
                    g.FillRectangle(Brushes.Green, 0, 0, 40, 40)
                End Using
            End If


            ' --- Carga de imagen de bala del jugador desde recursos ---
            If My.Resources.NewBala IsNot Nothing Then ' Usamos el nombre de tu recurso
                imagenBalaJugador = My.Resources.NewBala
            Else
                MessageBox.Show("Recurso 'BalaAliada' no encontrado. Usando bala predeterminada blanca.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                imagenBalaJugador = New Bitmap(5, 15) ' Creamos un Bitmap para usar como fallback
                Using g As Graphics = Graphics.FromImage(imagenBalaJugador)
                    g.FillRectangle(Brushes.White, 0, 0, 5, 15)
                End Using
            End If

            ' --- AHORA SÍ: Cargar imagen de bala enemiga desde recursos ---
            If My.Resources.BalaEne_removebg_preview IsNot Nothing Then ' Usamos el nombre de tu recurso
                imagenBalaEnemigo = My.Resources.BalaEne_removebg_preview
            Else
                MessageBox.Show("Recurso 'BalaEnemiga' no encontrado. Usando bala predeterminada roja.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                imagenBalaEnemigo = New Bitmap(8, 8) ' Creamos un Bitmap para usar como fallback
                Using g As Graphics = Graphics.FromImage(imagenBalaEnemigo)
                    g.FillEllipse(Brushes.Red, 0, 0, 8, 8)
                End Using
            End If


        Else
            MessageBox.Show("Algunos recursos de naves no fueron encontrados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Return
        End If


        ' Inicializar al jugador (Nave Aliada)
        Dim jugadorAncho As Integer = My.Resources.navealiada8.Width
        Dim jugadorAlto As Integer = My.Resources.navealiada8.Height
        Dim jugadorXInicial As Integer = (pbAreaJuego.Width \ 2) - (jugadorAncho \ 2)
        Dim jugadorYInicial As Integer = pbAreaJuego.Height - jugadorAlto - 20
        jugador = New Jugador(jugadorXInicial, jugadorYInicial, My.Resources.navealiada8, propulsionFrames, imagenEscudo, balaJugadorAnimacionFrames)


        If Me.Controls.ContainsKey("lblVidas") Then
            Me.lblVidas.Text = "Vidas: " & jugador.Vidas.ToString()
        End If
        If Me.Controls.ContainsKey("lblPuntuacion") Then
            Me.lblPuntuacion.Text = "Puntos: 0 x1"
        End If

        ' --- Inicializar el máximo de enemigos actual ---
        _maxEnemigosActual = _maxEnemigosInicial

        ' Se prepara la cola de enemigos
        GenerarColaEnemigos()
        Me.Refresh()
        TmrGameLoop.Enabled = True
        JuegoEnCurso = True                     ' para indicar que el juego está activo
    End Sub

    Private Sub GenerarColaEnemigos()
        ' Limpiar la cola actual antes de rellenar
        enemigosEnEspera.Clear()

        ' Generacion de mezcla de enemigos aleatorios para la cola
        For i As Integer = 1 To 10 ' Sigue generando 10 tipos de enemigos para la cola
            Dim tipoAleatorio As TipoEnemigo = listaTiposEnemigos(rnd.Next(listaTiposEnemigos.Count))

            ' Se crea el enemigo con la imagen y salud de su tipo.
            Dim nuevoEnemigo As New Enemigo(0, -50, tipoAleatorio.Imagen, tipoAleatorio.Salud)

            ' Asignar la velocidad de movimiento y de disparo específica del tipo
            nuevoEnemigo.Velocidad = tipoAleatorio.Velocidad
            enemigosEnEspera.Enqueue(nuevoEnemigo)
        Next
    End Sub

    ' Bucle principal del juego. Se ejecuta cada tick del Timer.
    Private Sub tmrGameLoop_Tick_1(ByVal sender As Object, ByVal e As EventArgs) Handles TmrGameLoop.Tick

        If Not JuegoEnCurso Then Return ' Si el juego no está en curso, no hagas nada
        If IsPaused Then Return ' Si el juego está pausado, salir del tick y no actualizar nada

        _gameTime += TmrGameLoop.Interval                                                                                ' Actualizar el tiempo total del juego
        _contadorTicks += 1                                                                                              ' Incrementar el contador de ticks

        ' --- ACTUALIZAR EL TEXTO DEL LABEL ---
        If Me.Controls.ContainsKey("lblContadorTicks") Then
            Me.lblContadorTicks.Text = "Kilometros: " & _contadorTicks.ToString() & _
                                       " | Tiempo: " & (_gameTime / 1000).ToString("F1") & "s"

        End If
        ' --Aumentar la velocidad del fondo gradualmente ---
        If _gameTime - _tiempoUltimoIncrementoVelocidad >= TIEMPO_PARA_INCREMENTO_VELOCIDAD_MS Then
            _velocidadFondoActual += INCREMENTO_VELOCIDAD_FONDO
            FondoD1.Speed = New Point(FondoD1.Speed.X, _velocidadFondoActual)                                            ' Actualizar la velocidad Y del fondo
            _tiempoUltimoIncrementoVelocidad = _gameTime
        End If

        ' --- Aumentar el máximo de enemigos en pantalla gradualmente ---
        If _gameTime - _tiempoUltimoIncrementoMaxEnemigos >= TIEMPO_PARA_INCREMENTO_MAX_ENEMIGOS_MS Then
            If _maxEnemigosActual < MAX_ENEMIGOS_FINAL Then
                _maxEnemigosActual += INCREMENTO_MAX_ENEMIGOS
                If _maxEnemigosActual > MAX_ENEMIGOS_FINAL Then
                    _maxEnemigosActual = MAX_ENEMIGOS_FINAL                                                              ' Asegurar que no exceda el máximo final
                End If
                _tiempoUltimoIncrementoMaxEnemigos = _gameTime
            End If
        End If


        ' Se actualizar el fondo de desplazable
        If FondoD1 IsNot Nothing Then
            FondoD1.Update()
        End If

        ' Actualizar Jugador
        If _moverDerecha Then
            jugador.Posicion = New Point(jugador.Posicion.X + jugador.Velocidad, jugador.Posicion.Y)
        ElseIf _moverIzquierda Then
            jugador.Posicion = New Point(jugador.Posicion.X - jugador.Velocidad, jugador.Posicion.Y)
        End If
        jugador.Update(0, pbAreaJuego.Width, _gameTime, TmrGameLoop.Interval)


        '  Actualizar Enemigos
        For i As Integer = enemigosActivos.Count - 1 To 0 Step -1
            Dim enemigo As Enemigo = enemigosActivos(i)
            enemigo.Update(jugador.Posicion.X + (jugador.Ancho \ 2), jugador.Posicion.Y + (jugador.Alto \ 2), _gameTime, imagenBalaEnemigo, pbAreaJuego.Height)


            ' --- LÓGICA DE ELIMINACIÓN DE ENEMIGOS ---
            If enemigo.EscapoDePantalla Then                                                                            ' Si el enemigo escapó de la pantalla
                MultiplicadorPuntuacion = 1                                                                             ' Reinicia el multiplicador
                ActualizarHUD()
                enemigosActivos.RemoveAt(i)                                                                             ' Eliminar este enemigo
                Continue For                                                                                            ' Pasa al siguiente enemigo, ya no hay más que hacer con este
            End If


            ' Para eliminar por CUALQUIER OTRA RAZÓN ( salud a 0 o por bala)
            If enemigo.DeberiaSerEliminado Then
                enemigosActivos.RemoveAt(i)                                                                             ' Eliminar este enemigo 
                Continue For                                                                                            ' Pasa inmediatamente al siguiente enemigo en el bucle
            End If

            ' ---  Transferir las balas recién disparadas por el enemigo --
            For Each nuevaBala As Bala In enemigo.BalasNuevas
                balasEnemigasActivas.Add(nuevaBala)
            Next
            enemigo.BalasNuevas.Clear() ' Vaciar la lista de balas del enemigo 

        Next


        ' --- Actualizar y limpiar explosiones activas ---
        For i As Integer = activeExplosions.Count - 1 To 0 Step -1
            Dim effect As AnimatedEffect = activeExplosions(i)
            effect.Animation.Update(_gameTime)                                                                          ' Actualizar la animación de cada explosión
            If effect.Animation.HasFinished Then                                                                        ' Si la animación de la explosión terminó (no loopea)
                activeExplosions.RemoveAt(i)                                                                            ' Eliminar el efecto de la lista
            End If
        Next

        ' --- Reducir el tiempo entre spawns gradualmente ---
        If _gameTime - _tiempoUltimaReduccionSpawn >= TIEMPO_PARA_REDUCIR_SPAWN_MS Then
            If _tiempoEntreSpawnActual > TIEMPO_SPAWN_MINIMO_MS Then
                _tiempoEntreSpawnActual -= REDUCCION_TIEMPO_SPAWN_MS
                If _tiempoEntreSpawnActual < TIEMPO_SPAWN_MINIMO_MS Then
                    _tiempoEntreSpawnActual = TIEMPO_SPAWN_MINIMO_MS
                End If
            End If
            _tiempoUltimaReduccionSpawn = _gameTime
        End If
        ' --- Actualizar y limpiar TODAS las balas enemigas activas ---
        For i As Integer = balasEnemigasActivas.Count - 1 To 0 Step -1
            Dim bala As Bala = balasEnemigasActivas(i)
            bala.Update()

            ' Eliminar balas enemigas que salen de la pantalla
            If bala.Posicion.Y > pbAreaJuego.Height + bala.Alto OrElse bala.Posicion.Y < -bala.Alto Then
                balasEnemigasActivas.RemoveAt(i)
            End If
        Next

        ' ---Actualizar Power-ups ---
        For i As Integer = activePowerUps.Count - 1 To 0 Step -1
            Dim pu As PowerUp = activePowerUps(i)
            pu.Update()

            ' Eliminar power-ups que salen de la pantalla por abajo
            If pu.Posicion.Y > pbAreaJuego.Height Then
                activePowerUps.RemoveAt(i)
            End If
        Next

        ' ---Spawneo de Power-ups  ---
        If _gameTime - _tiempoUltimoPowerUpSpawn >= TIEMPO_ENTRE_POWERUP_SPAWN_MS Then
            Dim randVal As Integer = rnd.Next(1, 11)
            If randVal <= PROBABILIDAD_POWERUP Then
                SpawnPowerUp()
            End If
            _tiempoUltimoPowerUpSpawn = _gameTime
        End If



        ' Spawneo de Enemigos 
        If enemigosActivos.Count < _maxEnemigosActual AndAlso enemigosEnEspera.Count > 0 Then
            If _gameTime - _tiempoUltimoSpawn >= _tiempoEntreSpawnActual Then
                Dim nuevoEnemigo As Enemigo = enemigosEnEspera.Dequeue()
                Dim spawnX As Integer = rnd.Next(0, pbAreaJuego.Width - nuevoEnemigo.Ancho)
                nuevoEnemigo.Posicion = New Point(spawnX, -nuevoEnemigo.Alto)

                enemigosActivos.Add(nuevoEnemigo)
                _tiempoUltimoSpawn = _gameTime

                If enemigosEnEspera.Count = 0 Then
                    GenerarColaEnemigos()
                End If
            End If
        End If


        DetectarColisiones()
        ActualizarHUD()
        pbAreaJuego.Invalidate()
    End Sub


    Private Sub SpawnPowerUp()
        Dim powerUpX As Integer = rnd.Next(0, pbAreaJuego.Width - 50)                            ' Ancho estimado de power-up
        Dim powerUpY As Integer = -50                                                            ' Aparece desde arriba de la pantalla

        Dim tipoAleatorio As PowerUpTipo
        Dim imagenSeleccionada As Image

        ' Decidir aleatoriamente qué tipo de power-up aparecerá
        If rnd.Next(0, 2) = 0 Then                                                               ' 0 para Escudo, 1 para DisparoDoble (50% de probabilidad para cada uno)
            tipoAleatorio = PowerUpTipo.Escudo
            imagenSeleccionada = imagenEscudo
        Else
            tipoAleatorio = PowerUpTipo.DisparoDoble
            imagenSeleccionada = imagenDobleDisparo
        End If

        ' Asegurarse de que tenemos una imagen válida antes de crear el PowerUp
        If imagenSeleccionada IsNot Nothing Then
            Dim nuevoPowerUp As New PowerUp(powerUpX, powerUpY, imagenSeleccionada, tipoAleatorio)
            activePowerUps.Add(nuevoPowerUp)
        End If
    End Sub


    '--------------------------------------------------------------------'
    ' Método para actualizar los Labels en la interfaz (HUD)
    Private Sub ActualizarHUD()
        ' Verificar si el jugador existe antes de intentar acceder a sus propiedades
        If jugador IsNot Nothing Then
            If Me.Controls.ContainsKey("lblVidas") Then
                Me.lblVidas.Text = "Vidas: " & jugador.Vidas.ToString()
            End If
            If Me.Controls.ContainsKey("lblPuntuacion") Then
                Me.lblPuntuacion.Text = "Puntos: " & PuntuacionActual.ToString() & "    Bonos: x" & MultiplicadorPuntuacion.ToString()
            End If
        Else
            ' Si el jugador es Nothing, puedes establecer los labels a un estado por defecto
            If Me.Controls.ContainsKey("lblVidas") Then
                Me.lblVidas.Text = "Vidas: -"
            End If
            If Me.Controls.ContainsKey("lblPuntuacion") Then
                Me.lblPuntuacion.Text = "Puntos: 0x1"
            End If
        End If
    End Sub

    Private Sub DetectarColisiones()
        'Colisiones: Balas del jugador vs Enemigos  
        Dim enemigosAEliminar As New List(Of Enemigo)()                                          ' Para acumular enemigos a eliminar
        Dim balasJugadorAEliminar As New List(Of Bala)()                                         ' Si las balas del jugador se consumen

        For iBala As Integer = jugador.Balas.Count - 1 To 0 Step -1
            Dim balaJugador As Bala = jugador.Balas(iBala)
            For iEnemigo As Integer = enemigosActivos.Count - 1 To 0 Step -1
                Dim enemigo As Enemigo = enemigosActivos(iEnemigo)

                If balaJugador.GetBounds().IntersectsWith(enemigo.GetBounds()) Then
                    ' Colisión detectada: bala impacta enemigo
                    balasJugadorAEliminar.Add(balaJugador)

                    ' Enemigo recibe daño
                    enemigo.RecibirDanio(1)

                    If enemigo.SaludActual <= 0 Then
                        enemigosAEliminar.Add(enemigo)                                           ' Marcar enemigo para eliminar si su salud llegó a 0
                        PuntuacionActual += VALOR_BASE_ENEMIGO * MultiplicadorPuntuacion         ' Sumar puntos y aumentar multiplicador
                        If MultiplicadorPuntuacion < MAX_MULTIPLICADOR Then
                            MultiplicadorPuntuacion += 1                                         ' Aumenta el multiplicador si no ha llegado al máximo
                        End If

                        ' ---  Crear una explosión en la posición del enemigo ---
                        If explosionFrames.Count > 0 Then
                            ' Posicionar la explosión en el centro del enemigo
                            Dim explosionX As Integer = enemigo.Posicion.X + (enemigo.Ancho \ 2) - (explosionFrames(0).Width \ 2)
                            Dim explosionY As Integer = enemigo.Posicion.Y + (enemigo.Alto \ 2) - (explosionFrames(0).Height \ 2)

                            ' Crear la instancia de Animacion (75ms por frame, NO loopear)
                            Dim explosionAnim As New Animacion(explosionFrames, 75, False)

                            ' Añadir a la lista de explosiones activas
                            activeExplosions.Add(New AnimatedEffect(explosionAnim, explosionX, explosionY, explosionFrames(0).Width, explosionFrames(0).Height))
                        End If
                    End If
                    Exit For
                End If
            Next
        Next


        ' Eliminar enemigos después de la iteración
        For Each enemigoAE As Enemigo In enemigosAEliminar
            If enemigosActivos.Contains(enemigoAE) Then
                enemigosActivos.Remove(enemigoAE)
            End If
        Next
        ' Eliminar balas del jugador (si se consumen al impactar)
        For Each balaAE As Bala In balasJugadorAEliminar
            If jugador.Balas.Contains(balaAE) Then
                jugador.Balas.Remove(balaAE)
            End If
        Next


        ' Colisiones: Balas de TODAS las balas enemigas vs Jugador ---
        For iBala As Integer = balasEnemigasActivas.Count - 1 To 0 Step -1
            Dim balaEnemiga As Bala = balasEnemigasActivas(iBala)
            If balaEnemiga.GetBounds().IntersectsWith(jugador.GetBounds()) Then                 ' Colisión detectada: bala enemiga impacta jugador
                balasEnemigasActivas.RemoveAt(iBala)                                            ' La bala enemiga sí se elimina al impactar al jugador
                jugador.RecibirDanio()                                                          ' El jugador pierde una vida
                ActualizarHUD()                                                                 ' Actualizar la interfaz inmediatamente

                If jugador.Vidas <= 0 Then                                                      ' ---Crear una explosión GRANDE en la posición del jugador ---
                    If explosionFrames.Count > 0 Then
                        Dim explosionX As Integer = jugador.Posicion.X + (jugador.Ancho \ 2) - (explosionFrames(0).Width \ 2)
                        Dim explosionY As Integer = jugador.Posicion.Y + (jugador.Alto \ 2) - (explosionFrames(0).Height \ 2)

                        ' Hacer la explosión un poco más grande para el jugador
                        Dim explosionWidth As Integer = CInt(explosionFrames(0).Width * 1.5)
                        Dim explosionHeight As Integer = CInt(explosionFrames(0).Height * 1.5)

                        ' Crear la instancia de Animacion (100ms por frame para un efecto más lento)
                        Dim explosionAnim As New Animacion(explosionFrames, 100, False)
                        activeExplosions.Add(New AnimatedEffect(explosionAnim, explosionX, explosionY, explosionWidth, explosionHeight))
                    End If
                    FinDelJuego("¡Juego Terminado! Colisionaste con un enemigo y perdiste todas tus vidas.")
                    Return
                End If
                Exit For
            End If
        Next

        ' Colisiones: Nave del jugador vs Naves enemigas
        For iEnemigo As Integer = enemigosActivos.Count - 1 To 0 Step -1
            Dim enemigo As Enemigo = enemigosActivos(iEnemigo)
            If jugador.GetBounds().IntersectsWith(enemigo.GetBounds()) Then                                         ' Colisión detectada: jugador impacta enemigo
                jugador.RecibirDanio()                                                                              ' Jugador pierde una vida (o directamente game over si la colisión es fatal)
                enemigosActivos.RemoveAt(iEnemigo)                                                                  ' Eliminar el enemigo con el que colisionó
                ActualizarHUD()                                                                                     ' Actualizar la interfaz inmediatamente

                If jugador.Vidas <= 0 Then                                                                          ' ¡Juego Terminado!
                    FinDelJuego("¡Juego Terminado! Colisionaste con un enemigo y perdiste todas tus vidas.")
                    Return                                                                                          ' Salir del método de colisiones
                End If
                Exit For                                                                                            ' Jugador colisionó, no necesitamos verificar más enemigos para esta colisión
            End If
        Next

        ' --- Colisiones: Jugador vs Power-ups ---
        For iPowerUp As Integer = activePowerUps.Count - 1 To 0 Step -1
            Dim currentPowerUp As PowerUp = activePowerUps(iPowerUp)

            If jugador.GetBounds().IntersectsWith(currentPowerUp.GetBounds()) Then                                  ' Colisión detectada: jugador recolecta power-up
                activePowerUps.RemoveAt(iPowerUp)                                                                   ' Eliminar el power-up de la lista

                Select Case currentPowerUp.Tipo
                    Case PowerUpTipo.Escudo
                        jugador.TieneEscudo = True
                        jugador.EscudoDuracionRestante = jugador.DURACION_ESCUDO_MS
                    Case PowerUpTipo.DisparoDoble
                        jugador.TieneDobleDisparo = True
                        jugador.SetDobleDisparoDuracion(jugador.DURACION_DOBLE_DISPARO_MS)
                End Select
            End If
        Next
    End Sub

    '--------------------------------------------'
    ' Método para manejar el fin del juego
    Private Sub FinDelJuego(ByVal mensaje As String)
        JuegoEnCurso = False                                                                                    ' Detener el juego
        TmrGameLoop.Enabled = False                                                                             ' Desactivar el temporizador del juego
        My.Computer.Audio.Stop()                                                                                ' Detener la música del juego


        IsPaused = False                                                                                        ' Asegurarse de que no esté pausado al terminar
        If pnlPausa IsNot Nothing Then
            pnlPausa.Visible = False                                                                            ' Ocultar el panel de pausa
        End If

        ' --- Lógica para puntajes altos ---
        If GestorPuntajes.EsPuntajeAlto(PuntuacionActual) Then
            Dim nombreJugador As String = InputBox("¡Felicidades! Has obtenido un puntaje alto." & vbCrLf & "Ingresa tu nombre:", "Nuevo Puntaje Alto", "Jugador")
            If Not String.IsNullOrWhiteSpace(nombreJugador) Then
                GestorPuntajes.GuardarNuevoPuntaje(New Puntuacion(nombreJugador, PuntuacionActual))
            End If
        End If
        ' ---------------------------------------
        ' Llamar a la pantalla de Game Over
        MostrarPantallaGameOver(mensaje)
    End Sub

    ' Método para mostrar la pantalla de Game Over
    Private Sub MostrarPantallaGameOver(ByVal mensajeExtra As String)
        Dim gameOverForm As New FormularioGameOver()
        gameOverForm.PuntuacionFinal = PuntuacionActual                                                         ' Pasar la puntuación al formulario Game Over

        ' Suscribirse a los eventos del formulario de Game Over
        AddHandler gameOverForm.ReintentarJuego, AddressOf HandleReintentarJuego
        AddHandler gameOverForm.VolverAlMenuPrincipal, AddressOf HandleVolverAlMenuPrincipal

        gameOverForm.ShowDialog()

        ' Desuscribirse de los eventos para evitar fugas de memoria (IMPORTANTE)
        RemoveHandler gameOverForm.ReintentarJuego, AddressOf HandleReintentarJuego
        RemoveHandler gameOverForm.VolverAlMenuPrincipal, AddressOf HandleVolverAlMenuPrincipal
        If Not JuegoEnCurso Then                                                                                ' Si JuegoEnCurso sigue siendo False (no se reintentó)
            Me.Close()                                                                                          ' Cerrar el formulario de juego
        End If
    End Sub

    ' Manejador para el evento ReintentarJuego
    Private Sub HandleReintentarJuego(ByVal sender As Object, ByVal e As EventArgs)
        ReiniciarJuego()
    End Sub

    ' Método para reiniciar el estado del juego
    Private Sub ReiniciarJuego()
        ' Resetear variables y objetos del juego
        _gameTime = 0
        _contadorTicks = 0
        PuntuacionActual = 0
        MultiplicadorPuntuacion = 1
        _velocidadFondoActual = _velocidadFondoInicial
        _tiempoUltimoIncrementoVelocidad = 0
        _tiempoEntreSpawnActual = 3000
        _tiempoUltimaReduccionSpawn = 0

        ' --- Resetear el máximo de enemigos ---
        _maxEnemigosActual = _maxEnemigosInicial
        _tiempoUltimoIncrementoMaxEnemigos = 0

        ' CLAVE PARA EL RESPAWN INMEDIATO DE ENEMIGOS!
        _tiempoUltimoSpawn = 0

        ' OCULTAR PANEL PAUSA
        IsPaused = False                                                                ' Asegurarse de que no esté pausado al reiniciar
        If pnlPausa IsNot Nothing Then                                                  ' Asegurarse de que el panel exista
            pnlPausa.Visible = False                                                    ' Ocultar el panel de pausa
        End If

        ' Reiniciar el jugador
        Dim jugadorAncho As Integer = My.Resources.navealiada8.Width
        Dim jugadorAlto As Integer = My.Resources.navealiada8.Height
        Dim jugadorXInicial As Integer = (pbAreaJuego.Width \ 2) - (jugadorAncho \ 2)
        Dim jugadorYInicial As Integer = pbAreaJuego.Height - jugadorAlto - 20
        jugador = New Jugador(jugadorXInicial, jugadorYInicial, My.Resources.navealiada8, propulsionFrames, imagenEscudo, balaJugadorAnimacionFrames) ' Crea un nuevo jugador con vidas iniciales

        ' Limpiar listas de enemigos y balas
        enemigosActivos.Clear()
        enemigosEnEspera.Clear()
        balasEnemigasActivas.Clear()
        activePowerUps.Clear()
        GenerarColaEnemigos()
        FondoD1.Inicializar(FondoStarsList, _velocidadFondoActual, AnchoFondo, AltoFondo)

        ' Reactivar el bucle de juego
        TmrGameLoop.Enabled = True
        JuegoEnCurso = True
        If My.Resources.MusicRock IsNot Nothing Then
            Try
                My.Computer.Audio.Play(My.Resources.MusicRock, AudioPlayMode.BackgroundLoop)
            Catch ex As Exception
                MessageBox.Show("Error al reproducir música del juego: " & ex.Message, "Error de Audio", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

        ActualizarHUD()
        pbAreaJuego.Invalidate()
    End Sub

    ' Manejador para el evento VolverAlMenuPrincipal
    Private Sub HandleVolverAlMenuPrincipal(ByVal sender As Object, ByVal e As EventArgs)
        JuegoEnCurso = False
    End Sub

    ' Maneja las teclas presionadas por el jugador.
    Private Sub FormularioJuego_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        If Not JuegoEnCurso Then Return
        Select Case e.KeyCode
            Case Keys.Right
                If Not IsPaused Then             ' Solo mover si NO está pausado
                    _moverDerecha = True
                End If
            Case Keys.Left
                If Not IsPaused Then             ' Solo mover si NO está pausado
                    _moverIzquierda = True
                End If
            Case Keys.Space
                If Not IsPaused Then             ' Solo disparar si NO está pausado
                    jugador.Disparar(imagenBalaJugador, _gameTime)
                End If
            Case Keys.P, Keys.Escape             ' Estas teclas siempre deben manejar la pausa si el juego está en curso
                TogglePause()
                e.Handled = True                 ' Consumir el evento de tecla para que no se propague
        End Select

    End Sub

    ' Maneja las teclas liberadas por el jugador.
    Private Sub FormularioJuego_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyUp
        If Not JuegoEnCurso Then Return ' Asegura que solo se procesen las teclas si el juego está activo
        If IsPaused Then Return
        Select Case e.KeyCode
            Case Keys.Right
                _moverDerecha = False
            Case Keys.Left
                _moverIzquierda = False

        End Select
    End Sub

    ' Manejar el cierre del formulario para liberar recursos
    Private Sub FormularioJuego_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        '  Detener el timer del juego
        TmrGameLoop.Enabled = False
        TmrGameLoop.Stop()

        ' Limpiar listas de objetos del juego
        enemigosActivos.Clear()
        enemigosEnEspera.Clear()
        balasEnemigasActivas.Clear()

        ' Eliminar referencias a objetos grandes o recursos para liberar memoria
        If FondoD1 IsNot Nothing Then
            FondoD1 = Nothing
        End If

        If jugador IsNot Nothing Then
            jugador = Nothing
        End If

        If imagenBalaEnemigo IsNot Nothing Then
            imagenBalaEnemigo = Nothing
        End If
        If imagenBalaJugador IsNot Nothing Then
            imagenBalaJugador = Nothing
        End If
        imagenesEnemigos.Clear()

        ' --Limpiar Power-ups ---
        activePowerUps.Clear()
        If imagenEscudo IsNot Nothing Then
            imagenEscudo.Dispose()
            imagenEscudo = Nothing
        End If
        If imagenDobleDisparo IsNot Nothing Then
            imagenDobleDisparo.Dispose()
            imagenDobleDisparo = Nothing
        End If
        balaJugadorAnimacionFrames.Clear()

        ' Detener cualquier sonido o música que esté reproduciéndose
        Try
            My.Computer.Audio.Stop()
        Catch ex As Exception
        End Try

        explosionFrames.Clear()
        propulsionFrames.Clear()
        activeExplosions.Clear()

    End Sub

    ' ----AREA DONDE SE DIBUJARA EL JUEGO
    Private Sub pbAreaJuego_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pbAreaJuego.Paint
        If IsInDesignMode OrElse FondoD1 Is Nothing Then
            e.Graphics.FillRectangle(Brushes.DarkGray, 0, 0, pbAreaJuego.Width, pbAreaJuego.Height)
            Return
        End If
        Dim g As Graphics = e.Graphics

        ' Dibujar el fondo desplazable primero
        If FondoD1 IsNot Nothing Then

            FondoD1.DRAW(g)
        End If


        ' Dibujar jugador
        If jugador IsNot Nothing Then

            jugador.Draw(g)
        End If

        ' Dibujar enemigos
        For Each enemigo As Enemigo In enemigosActivos
            enemigo.Draw(g)
        Next

        ' ---  Dibujar Power-ups ---
        For Each pu As PowerUp In activePowerUps
            pu.Draw(g)
        Next


        ' Dibujar las balas del jugador
        If jugador IsNot Nothing Then
            For Each bala As Bala In jugador.Balas
                bala.Draw(g)
            Next
        End If

        ' --- Dibujar las balas de los enemigos desde la lista global ---
        For Each bala As Bala In balasEnemigasActivas
            bala.Draw(g)
        Next

        ' --- Dibujar explosiones activas ---
        For Each effect As AnimatedEffect In activeExplosions
            effect.Animation.Draw(g, effect.Position.X, effect.Position.Y, effect.Width, effect.Height)
        Next

    End Sub

    '---METODO PAUSA---'
    Private Sub TogglePause()
        IsPaused = Not IsPaused                                     ' Invertir el estado de pausa

        If IsPaused Then
            ' Pausar el juego
            TmrGameLoop.Enabled = False                             ' Detener el bucle principal del juego
            My.Computer.Audio.Stop()                                ' Detener la música de fondo (opcional, algunos juegos bajan el volumen)
            pnlPausa.Visible = True                                 ' Mostrar el panel de pausa
            pnlPausa.BringToFront()                                 ' Asegurarse de que esté encima de todo

            ' Reiniciar el estado de movimiento del jugador al pausar ---
            _moverDerecha = False
            _moverIzquierda = False
        Else
            ' Reanudar el juego
            pnlPausa.Visible = False                                ' Ocultar el panel de pausa
            TmrGameLoop.Enabled = True                              ' Reanudar el bucle principal del juego
            ' Reanudar la música de fondo (reproducirla desde el inicio en modo bucle)
            If My.Resources.MusicRock IsNot Nothing Then
                Try
                    My.Computer.Audio.Play(My.Resources.MusicRock, AudioPlayMode.BackgroundLoop)
                Catch ex As Exception
                    Debug.WriteLine("Error al reanudar música del juego: " & ex.Message)
                End Try
            End If

            _moverDerecha = False
            _moverIzquierda = False
        End If
    End Sub


    Private Sub btnReanudar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReanudar.Click
        TogglePause()
    End Sub

    Private Sub btnPausaVolverMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPausaVolverMenu.Click

        Dim resultado As DialogResult = MessageBox.Show("¿Estás seguro de que quieres volver al menú principal? Perderás el progreso actual.", "Volver al Menú", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If resultado = DialogResult.Yes Then
            Me.Close()
        Else
        End If
    End Sub

   
End Class