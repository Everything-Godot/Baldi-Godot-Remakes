extends Control

@onready var camera = $".."

func _process(_delta: float) -> void:
	visible = $"Left joystick".visible

func _on_jump_pressed() -> void:
	Input.action_press("jump")
	Input.action_release("jump")

func _on_debug_pressed() -> void:
	var skip: bool = false
	for child in camera.get_children():
		if child.name == "Debug menu":
			camera.remove_child(child)
			skip = true
			break
	if not skip:
		var dm: PackedScene = load("res://scenes/debug_menu_android.tscn")
		var dm_node: Control = dm.instantiate()
		dm_node.name = "Debug menu"
		camera.add_child(dm_node)
