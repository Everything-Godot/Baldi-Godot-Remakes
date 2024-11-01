extends Sprite3D

var opened : bool
var open_texture = load("res://sprites/Door_80.png")
var close_texture = load("res://sprites/Door_0.png")
var open_snd = load("res://sounds/door_open.wav")
var close_snd = load("res://sounds/door_close.wav")
@onready var close_collision = $StaticBody3D/CollisionShape3DClosed
@onready var open1_collision = $StaticBody3D/CollisionShape3DOpened1
@onready var open2_collision = $StaticBody3D/CollisionShape3DOpened2
@onready var timer = $Timer2
@onready var sound = $AudioStreamPlayer3D2
var changed = false
var opening = false

func _ready() -> void:
	texture = close_texture
	close_collision.disabled = false
	open1_collision.disabled = true
	open2_collision.disabled = true

func _process(_delta: float) -> void:
	opened = get_meta("opened")
	if opened and not opening:
		opening = true
		texture = open_texture
		sound.stream = open_snd
		close_collision.disabled = true
		open1_collision.disabled = false
		open2_collision.disabled = false
		sound.play()
		timer.start(4)
		await timer.timeout
		texture = close_texture
		sound.stream = close_snd
		close_collision.disabled = false
		open1_collision.disabled = true
		open2_collision.disabled = true
		sound.play()
		changed = true
		opening = false
		set_meta("opened", false)
