extends CharacterBody3D

@onready var player = $"."
@onready var camera = $"Camera3D"
@export_category("player")
@export var speed : float = 5.0
@export var gravity : float = 20.0
@export var jump_speed : float = 5.0
var jumping := false
var movement_vector := Vector2.ZERO
var last_floor := false
var parent : Node
var start_check := false

func _ready() -> void:
	Input.mouse_mode = Input.MOUSE_MODE_CAPTURED

func _process(_delta: float) -> void:
	if Global.debug:
		get_tree().debug_collisions_hint = true
		get_tree().debug_navigation_hint = true
		get_tree().debug_paths_hint = true

func _physics_process(delta: float) -> void:
	if not Global.paused:
		if not Global.freelook:
			if start_check:
				if Input.is_action_pressed("interact"):
					parent.set_meta("opened", true)
			velocity.y += -gravity * delta
			var vy = velocity.y
			velocity.y = 0
			var inputDir = Input.get_vector("left", "right", "forward", "backward")
			var relativeDir = Vector3(inputDir.x, 0.0, inputDir.y).rotated(Vector3.UP, camera.rotation.y)
			velocity = lerp(velocity, relativeDir * speed, speed * delta)
			velocity.y = vy
			if is_on_floor() and not last_floor:
				jumping = false
			last_floor = is_on_floor()
			if is_on_floor() and Input.is_action_just_pressed("jump"):
				velocity.y = jump_speed
				jumping = true
		else:
			velocity = Vector3(0, 0, 0)
		move_and_slide()

func _on_area_3d_area_entered(area:Area3D) -> void:
	print(area)
	parent = area.get_parent()
	print(parent)
	if not Global.paused:
		if not Global.freelook:
			if parent.has_meta("opened"):
				print(parent.get_meta("opened"))
				if not parent.get_meta("opened"):
					start_check = true

func _on_area_3d_area_exited(area: Area3D) -> void:
	parent = area.get_parent()
	start_check = false
