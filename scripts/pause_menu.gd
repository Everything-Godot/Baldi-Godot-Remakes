extends Control

var title_screen = preload("res://scenes/title_screen.tscn")
@onready var camera = $".."
@onready var android = $Android
@onready var other = $Other
@onready var exit_button = $Android/Exit/Button

func _ready() -> void:
	if Global.os_name == "Android":
		android.visible = true
		other.visible = false
	else:
		android.visible = false
		other.visible = true

func _process(_delta: float) -> void:
	if not Global.paused:
		Global.paused = true
	if Input.is_key_pressed(KEY_Q) or exit_button.button_pressed:
		Global.paused = false
		Global.freelook = false
		get_tree().change_scene_to_file("res://scenes/title_screen.tscn")