extends Node3D

@onready var player = $"."
@onready var camera : Camera3D = $Camera3D
var mouse_sens = 0.3
var camera_anglev=0

func _input(event):  		
	if event is InputEventMouseMotion:
		camera.rotate_y(deg_to_rad(-event.relative.x*mouse_sens))
		var changev=-event.relative.y*mouse_sens
		#if camera_anglev+changev>-50 and camera_anglev+changev<50:
			#camera_anglev+=changev
		camera.rotate_x(deg_to_rad(changev))

func _ready() -> void:
	Input.mouse_mode = Input.MOUSE_MODE_CAPTURED

func _process(_delta: float) -> void:
	if Input.is_action_just_pressed("toggle fullscreen"):
		get_tree().exit()
