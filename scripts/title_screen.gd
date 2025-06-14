extends Node2D

const SCHOOLHOUSE_SCENE_PATH := "res://scenes/places/school_house.tscn"
@onready var slider = $"Title/Mouse/Slider"
@onready var title = $"Title"
@onready var exit_bnt = $Title/Buttons/Exit
@onready var start_bnt = $Title/Buttons/Start
@onready var howto_bnt = $"Title/Buttons/How to play"
@onready var howto = $"HowTo"

func _ready() -> void:
	ResourceLoader.load_threaded_request(SCHOOLHOUSE_SCENE_PATH, "", true)
	Input.mouse_mode = Input.MOUSE_MODE_VISIBLE
	title.visible = true
	howto.visible = false

func _process(delta: float) -> void:
	Global.mouse_sensitivity = slider.value
	await get_tree().create_timer(0.2 * delta).timeout

func _on_exit_pressed() -> void:
	if Global.os_name == "Web":
		var location = JavaScriptBridge.get_interface("location")
		location.href = "about:blank"
	else:
		get_tree().quit()

func _on_start_pressed() -> void:
	if ResourceLoader.load_threaded_get_status(SCHOOLHOUSE_SCENE_PATH) == 3:
		var school_house = ResourceLoader.load_threaded_get(SCHOOLHOUSE_SCENE_PATH)
		get_tree().change_scene_to_packed(school_house)

func _on_how_to_play_pressed() -> void:
	title.visible = false
	howto.visible = true

func _on_back_pressed() -> void:
	title.visible = true
	howto.visible = false

func _on_button_pressed() -> void:
	var random := RandomNumberGenerator.new()
	print("Generating random number")
	var i = random.randi_range(0, 2)
	print("Generated! result: " + str(i))
	if i == 0:
		_on_exit_pressed()
	elif i == 1:
		_on_start_pressed()
	else:
		_on_how_to_play_pressed()
