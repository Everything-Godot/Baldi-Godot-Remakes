extends Node3D

@onready var player = $"."
@onready var camera : Camera3D = $Camera3D
var mouse_sens = 0.3

func _input(event):  		
	if event is InputEventMouseMotion:
		camera.rotate_y(deg_to_rad(-event.relative.x*mouse_sens))

func _ready() -> void:
	Input.mouse_mode = Input.MOUSE_MODE_CAPTURED

func _process(_delta: float) -> void:
	if Input.is_key_pressed(KEY_ESCAPE):
		get_tree().quit()
	if EngineDebugger.is_active():
		if Input.is_action_pressed("forward"):
			player.position.x += 0.1
		if Input.is_action_pressed("backward"):
			player.position.x -= 0.1
		if Input.is_action_pressed("up"):
			player.position.y += 0.1
		if Input.is_action_pressed("down"):
			player.position.y -= 0.1
		if Input.is_action_pressed("left"):
			player.position.z += 0.1
		if Input.is_action_pressed("right"):
			player.position.z -= 0.1
		if Input.is_action_pressed("scroll up"):
			camera.position.z += 0.1
			camera.position.y += 0.05
			camera.position.x += 0.08
		if Input.is_action_pressed("scroll down"):
			camera.position.z -= 0.1
			camera.position.y -= 0.05
			camera.position.x -= 0.08
