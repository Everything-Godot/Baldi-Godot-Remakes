extends Control

var title_screen = preload("res://scenes/title_screen.tscn")
@onready var camera = $".."
@onready var this = $"."

func _process(_delta: float) -> void:
	if not Global.paused:
		Global.paused = true
	if Input.is_key_pressed(KEY_Q):
		Global.paused = false
		Global.freelook = false
		get_tree().change_scene_to_file("res://scenes/title_screen.tscn")
