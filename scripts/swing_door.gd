extends Sprite3D

var opened : bool
var locked : bool
var open_texture = load("res://sprites/SwingDoor60.png")
var close_texture = load("res://sprites/SwingDoor0.png")
var locked_texture = load("res://sprites/SwingDoor0_Locked.png")
var open_snd = load("res://sounds/swingdoor_open.wav")
var close_snd = load("res://sounds/door_close.wav")
var need_notebooks_snd = load("res://sounds/BAL_Door.wav")
@onready var close_collision = $StaticBody3D/CollisionShape3DClosed
@onready var open1_collision = $StaticBody3D/CollisionShape3DOpened1
@onready var open2_collision = $StaticBody3D/CollisionShape3DOpened2
@onready var timer = $Timer
@onready var sound = $AudioStreamPlayer3D
var changed = false
var opening = false

func _ready() -> void:
	texture = close_texture
	close_collision.disabled = false
	open1_collision.disabled = true
	open2_collision.disabled = true

func _process(_delta: float) -> void:
	locked = get_meta("Locked")
	opened = get_meta("opened")
	if locked:
		set_meta("opened", false)
		texture = locked_texture
	else:
		if opened:
			if not sound.playing:
				print("Door opening, start check functions for swing doors.")
				if Global.notebooks >= 2:
					print("Enough notebooks, start opening door.")
					if not opening:
						opening = true
						texture = open_texture
						sound.stream = open_snd
						close_collision.disabled = true
						open1_collision.disabled = false
						open2_collision.disabled = false
						sound.play()
						timer.start(3)
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
				else:
					print("Not enough notebooks! Start playing sounds.")
					sound.stream = need_notebooks_snd
					sound.play()
					await sound.finished
					set_meta("opened", false)
			else:
				await sound.finished
				set_meta("opened", false)
