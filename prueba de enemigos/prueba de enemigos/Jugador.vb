Imports System.Drawing

Public Class Jugador
    Public Posicion As Point
    Public ImagenNave As Image
    Public Ancho As Integer
    Public Alto As Integer
    Public Velocidad As Integer = 8                                             ' Velocidad de movimiento del jugador
    Public Balas As New List(Of Bala)                                           ' Lista de balas disparadas por el jugador
    Public Property Vidas As Integer                                            ' Vida del jugador

    Private _ultimoDisparoTiempo As Long                                        ' Para controlar el tiempo entre disparos
    Private Const TIEMPO_ENTRE_DISPAROS_MS As Long = 300                        ' Cada cuánto puede disparar el jugador (milisegundos)

    Public PropulsionAnimacion As Animacion                                     '  La instancia de la animación de propulsión

    Public Property BalaAnimacionFrames As List(Of Image)
    Public Property BalaAnimacionFrameDuration As Long = 100                    ' Duración por frame de la animación de la bala


    ' --- PROPIEDADES PARA POWER-UPS ---
    Public Property TieneEscudo As Boolean = False
    Public Property EscudoDuracionRestante As Long = 0                          ' Tiempo restante del escudo en milisegundos
    Public Const DURACION_ESCUDO_MS As Long = 10000                             ' Duración del escudo: 10 segundos

    Public Property TieneDobleDisparo As Boolean = False
    Private _disparoDobleDuracionRestante As Long = 0                           ' Tiempo restante del doble disparo en milisegundos
    Public Const DURACION_DOBLE_DISPARO_MS As Long = 7000                       ' Duración del doble disparo: 7 segundos

    ' Para el dibujado del escudo
    Public Property ImagenEscudo As Image

    Public Sub New(ByVal x As Integer, ByVal y As Integer, ByVal img As Image, ByVal propFrames As List(Of Image), ByVal imgEscudo As Image, Optional ByVal balaAnimFrames As List(Of Image) = Nothing)
        Me.Posicion = New Point(x, y)
        Me.ImagenNave = img
        Me.Ancho = img.Width
        Me.Alto = img.Height
        Me.Vidas = 3

        Me.ImagenEscudo = imgEscudo                             ' ASIGNAR LA IMAGEN DEL ESCUDO
        Me.BalaAnimacionFrames = balaAnimFrames                 ' ASIGNAR LOS FRAMES DE ANIMACIÓN DE LA BALA


        If propFrames IsNot Nothing AndAlso propFrames.Count > 0 Then
            ' Crear la animación de propulsión: 100ms por frame, en BUCLE (True)
            Me.PropulsionAnimacion = New Animacion(propFrames, 100, True)
        Else
            Me.PropulsionAnimacion = Nothing                    ' No hay frames, no crear animación
        End If
    End Sub

    ' Actualiza el estado del jugador (movimiento)
    Public Sub Update(ByVal minX As Integer, ByVal maxX As Integer, ByVal currentTime As Long, ByVal gameLoopIntervalMs As Long)
        ' Limitar la posición del jugador dentro de la pantalla
        If Posicion.X < minX Then Posicion = New Point(minX, Posicion.Y)
        If Posicion.X + Ancho > maxX Then Posicion = New Point(maxX - Ancho, Posicion.Y)

        ' Actualizar el estado de las balas del jugador
        For i As Integer = Balas.Count - 1 To 0 Step -1
            Dim bala As Bala = Balas(i)                                         ' Asegúrate de tener una variable para la bala actual
            bala.Update(currentTime)
            ' Eliminar balas que salieron de la pantalla
            If bala.Posicion.Y < 0 Then
                Balas.RemoveAt(i)
            End If
        Next

        ' ---  Actualizar la duración del escudo ---
        If TieneEscudo Then
            EscudoDuracionRestante -= gameLoopIntervalMs
            If EscudoDuracionRestante <= 0 Then
                TieneEscudo = False
                EscudoDuracionRestante = 0
            End If
        End If

        ' --- Actualizar la duración del doble disparo ---
        If TieneDobleDisparo Then
            _disparoDobleDuracionRestante -= gameLoopIntervalMs ' Restar el intervalo del timer
            If _disparoDobleDuracionRestante <= 0 Then
                TieneDobleDisparo = False
                _disparoDobleDuracionRestante = 0
            End If
        End If

        ' ---  Actualizar la animación de propulsión ---
        If PropulsionAnimacion IsNot Nothing Then
            PropulsionAnimacion.Update(currentTime)
        End If
    End Sub


    ' Dibuja la nave del jugador y sus balas.
   
    Public Sub Draw(ByVal g As Graphics)
        g.DrawImage(Me.ImagenNave, Me.Posicion.X, Me.Posicion.Y, Me.Ancho, Me.Alto)

        ' --- Dibujar el escudo si está activo ---
        If TieneEscudo AndAlso Me.ImagenEscudo IsNot Nothing Then
            ' Ajusta la posición y tamaño del escudo para que envuelva la nave
            Dim margenEscudo As Integer = 10
            Dim escudoX As Integer = Me.Posicion.X - margenEscudo
            Dim escudoY As Integer = Me.Posicion.Y - margenEscudo
            Dim escudoAncho As Integer = Me.Ancho + (margenEscudo * 2)
            Dim escudoAlto As Integer = Me.Alto + (margenEscudo * 2)

            g.DrawImage(Me.ImagenEscudo, escudoX, escudoY, escudoAncho, escudoAlto)
        End If

        ' --- Dibujar la animación de propulsión ---
        If PropulsionAnimacion IsNot Nothing AndAlso Not PropulsionAnimacion.HasFinished Then
            ' Calcular la posición de la propulsión para que esté en la parte inferior central de la nave
            Dim propX As Integer = Me.Posicion.X + (Me.Ancho \ 2) - (PropulsionAnimacion.CurrentFrame.Width \ 2)
            Dim propY As Integer = Me.Posicion.Y + Me.Alto - (PropulsionAnimacion.CurrentFrame.Height \ 4)

            Dim propWidth As Integer = PropulsionAnimacion.CurrentFrame.Width
            Dim propHeight As Integer = PropulsionAnimacion.CurrentFrame.Height

            PropulsionAnimacion.Draw(g, propX, propY, propWidth, propHeight)
        End If

        For Each bala As Bala In Balas
            bala.Draw(g)
        Next
    End Sub

    ' Dispara una bala si ha pasado suficiente tiempo desde el último disparo.
    Public Sub Disparar(ByVal balaImagen As Image, ByVal currentTime As Long)
        If currentTime - _ultimoDisparoTiempo >= TIEMPO_ENTRE_DISPAROS_MS Then
            If BalaAnimacionFrames Is Nothing OrElse BalaAnimacionFrames.Count = 0 Then
                
                MessageBox.Show("Advertencia: No hay frames de animación de bala cargados para el jugador.", "Error de Animación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Debug.WriteLine("BalaAnimacionFrames.Count: " & BalaAnimacionFrames.Count)
            Debug.WriteLine("BalaAnimacionFrameDuration: " & BalaAnimacionFrameDuration)

            Dim balaAncho As Integer = BalaAnimacionFrames(0).Width
            Dim balaAlto As Integer = BalaAnimacionFrames(0).Height


            If Not TieneDobleDisparo Then
                ' Disparo normal
                Dim balaX As Integer = Me.Posicion.X + (Me.Ancho \ 2) - (balaAncho \ 2)
                Dim balaY As Integer = Me.Posicion.Y - balaAlto

                Dim nuevaBala As New Bala(balaX, balaY, 0, -10, True, Nothing, BalaAnimacionFrames, BalaAnimacionFrameDuration)
                Balas.Add(nuevaBala)
            Else
                ' Doble disparo (dos balas, una a cada lado)
                Dim offset As Integer = 15 ' Separación entre las balas
                Dim bala1X As Integer = Me.Posicion.X + (Me.Ancho \ 2) - balaAncho - (offset \ 2)
                Dim bala2X As Integer = Me.Posicion.X + (Me.Ancho \ 2) + (offset \ 2)
                Dim balaY As Integer = Me.Posicion.Y - balaAlto
               
                Dim balaIzquierda As New Bala(bala1X, balaY, 0, -10, True, Nothing, BalaAnimacionFrames, BalaAnimacionFrameDuration)
                Dim balaDerecha As New Bala(bala2X, balaY, 0, -10, True, Nothing, BalaAnimacionFrames, BalaAnimacionFrameDuration)

                Balas.Add(balaIzquierda)
                Balas.Add(balaDerecha)
            End If

            _ultimoDisparoTiempo = currentTime
        End If

    End Sub
    'Obtiene el rectángulo de colisión de la nave del jugador.
    Public Function GetBounds() As Rectangle
        Return New Rectangle(Me.Posicion.X, Me.Posicion.Y, Me.Ancho, Me.Alto)
    End Function

    '------------------------------------------'
    ' Método para que el jugador reciba daño
    Public Sub RecibirDanio()
        If TieneEscudo Then
            ' Si tiene escudo, el escudo absorbe el daño y se desactiva
            TieneEscudo = False
            EscudoDuracionRestante = 0

        Else
            ' Si no tiene escudo, pierde una vida
            Me.Vidas -= 1
        End If
    End Sub

    Public Sub SetDobleDisparoDuracion(ByVal duracionMs As Long)
        _disparoDobleDuracionRestante = duracionMs
    End Sub
End Class

