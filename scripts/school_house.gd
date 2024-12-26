extends Node3D

@onready var music : AudioStreamPlayer = $Music
@onready var notebook : Node3D = $Notebook
@onready var player_camera = $"Player/Camera3D"
@onready var pause_button : Button = $"Player/Camera3D/Android Control UI/Pause Button/Button"
@onready var notebook_counter : Label = $"Player/Camera3D/Notebook counter"
@onready var baldi_tour : AnimatedSprite3D = $Baldi_Tour/Sprite3D
@onready var pickups_node : Node3D = $Pickups
@export var audios : Array[Node]
@export var timers : Array[Node]
@export var pickups : Array[Node]
var prize_sound = load("res://sounds/BAL_GetPrize.wav")
var prize_sound2 = load("res://sounds/BAL_AllNotebooks.wav")
var empty_sound = load("res://sounds/delay.wav")
var area3d : Area3D
var played_prize : bool = false
var congrats_notebook : bool = false

func _ready() -> void:
	pickups = pickups_node.pickups
	audios.append($Doors/Door/AudioStreamPlayer3D)
	audios.append($"Doors/Swing Door/AudioStreamPlayer3D")
	audios.append($"Baldi_Tour/Baldi Audio")
	timers.append($Doors/Door/Timer)
	timers.append($"Doors/Swing Door/Timer")
	area3d = notebook.get_child(1)
	Global.notebooks = 0
	Global.already_wrong = false
	area3d.monitorable = true
	notebook.visible = true
	for pickup in pickups:
		if pickup.name == "Quarter":
			pickup.visible = false
			break

func _process(_delta: float) -> void:
	notebook_counter.text = str(Global.notebooks) + "/" + str(Global.total_notebooks) + " Notebooks"
	if Global.already_wrong:
		music.stop()
		music.stream = empty_sound
	if Global.notebooks == Global.total_notebooks and not congrats_notebook:
		for audio in audios:
			if audio.name == "Baldi Audio 2":
				congrats_notebook = true
				audio.stream = prize_sound2
				audio.play()
				break
			else:
				continue
		baldi_tour.play("default")
	if Global.notebooks >= Global.total_notebooks:
		notebook.visible = false
		area3d.monitorable = false
	else:
		notebook.visible = true
		area3d.monitorable = true
	if not Global.paused:
		if Global.notebooks == 1 and not Global.already_wrong and not played_prize:
			for audio in audios:
				if audio.name == "Baldi Audio":
					played_prize = true
					audio.stream = prize_sound
					audio.play()
					break
				else:
					continue
			for pickup in pickups:
				pickup.visible = true
			baldi_tour.play("default")
		notebook_counter.visible = true
		for audio in audios:
			if audio.stream_paused:
				audio.stream_paused = false
		for timer in timers:
			if timer.paused:
				timer.paused = false
		if Input.is_action_just_pressed("Esc") or pause_button.button_pressed:
			var pm_file = load("res://scenes/UI/pause_menu.tscn")
			var pm = pm_file.instantiate()
			pm.name = "PM"
			player_camera.add_child(pm)
	else:
		for timer in timers:
			timer.paused = true
		if Global.in_yctp:
			notebook_counter.visible = false
			for audio in audios:
				audio.stream_paused = true
		else:
			notebook_counter.visible = true
			for audio in audios:
				if audio.name == "Music":
					continue
				if audio.name == "Baldi Audio":
					continue
				audio.stream_paused = true

func _on_music_finished() -> void:
	if Global.already_wrong:
		music.stop()
		music.stream = empty_sound
	else:
		music.play()
