extends Node2D

var school_house = preload("res://scenes/school_house.tscn")
@onready var slider = $"Title/Mouse/Slider"
@onready var title = $"Title"
@onready var exit_bnt = $Title/Buttons/Exit
@onready var start_bnt = $Title/Buttons/Start
@onready var howto_bnt = $"Title/Buttons/How to play"
@onready var howto = $"HowTo"

func _ready() -> void:
	Input.mouse_mode = Input.MOUSE_MODE_VISIBLE
	title.visible = true
	howto.visible = false

func _process(delta: float) -> void:
	Global.mouse_sensitivity = slider.value
	await get_tree().create_timer(0.2*delta).timeout

func _on_exit_pressed() -> void:
	if Global.os_name == "Web":
		var location = JavaScriptBridge.get_interface("location")
		location.href = "about:blank"
	else:
		get_tree().quit()

func _on_start_pressed() -> void:
	get_tree().change_scene_to_packed(school_house)

func _on_how_to_play_pressed() -> void:
	title.visible = false
	howto.visible = true

func _on_back_pressed() -> void:
	title.visible = true
	howto.visible = false
