extends Node2D

var title_screen = preload("res://scenes/title_screen.tscn")
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if Input.is_anything_pressed():
		get_tree().change_scene_to_packed(title_screen)
