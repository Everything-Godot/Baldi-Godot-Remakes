extends Node2D

var title_screen = preload("res://scenes/title_screen.tscn")
@onready var warning = $Warning

func _process(_delta: float) -> void:
	if Input.is_anything_pressed():
		get_tree().change_scene_to_packed(title_screen)
