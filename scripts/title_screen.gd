extends Node2D

@onready var slider = $"Title/Mouse/Slider"
@onready var title = $"Title"
@onready var howto = $"HowTo"

func _ready() -> void:
	title.visible = true
	howto.visible = false

func _process(delta: float) -> void:
	Global.mouse_sensitivity = slider.value
	await get_tree().create_timer(0.2*delta).timeout

func _on_exit_pressed() -> void:
	get_tree().quit()

func _on_start_pressed() -> void:
	pass

func _on_how_to_play_pressed() -> void:
	title.visible = false
	howto.visible = true

func _on_back_pressed() -> void:
	title.visible = true
	howto.visible = false
