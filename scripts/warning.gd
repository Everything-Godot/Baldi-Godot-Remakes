extends Node2D

var title_screen = preload("res://scenes/title_screen.tscn")
@onready var warning = $Warning

func _process(_delta: float) -> void:
	if Input.is_anything_pressed():
		get_tree().change_scene_to_packed(title_screen)
	warning.position = Vector2((DisplayServer.window_get_size().x + 2 - 1152) / 4 + 150, 0)
