extends Node3D

@onready var sprite = $Sprite3D
@onready var audio = $"Baldi Audio"
@onready var timer = $Timer
var paused_before := false

func _ready() -> void:
	timer.start()
	audio.play()
	sprite.play("hi")
	await timer.timeout
	timer.stop()
	sprite.play("default")

func _process(_delta: float) -> void:
	#From GPT-3.5, if works then thanks
	var camera_global_transform = get_viewport().get_camera_3d().global_transform
	var camera_rotation = camera_global_transform.basis.get_euler()
	rotation = camera_rotation
	#GPT stops here
	if Global.paused:
		timer.paused = true
		sprite.pause()
		paused_before = true
	elif not Global.paused and paused_before:
		timer.paused = false
		sprite.play()
		paused_before = false
