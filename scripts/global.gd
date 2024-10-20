extends Node

var first_time : bool = true
var mouse_sensitivity : float
var debug : bool = EngineDebugger.is_active()
var paused : bool = false
var freelook : bool = false

func _process(_delta: float) -> void:
	if Input.is_action_just_pressed("toggle fullscreen") and debug and not paused:
		if DisplayServer.window_get_mode() == DisplayServer.WINDOW_MODE_FULLSCREEN:
			DisplayServer.window_set_mode(DisplayServer.WINDOW_MODE_WINDOWED)
		else:
			DisplayServer.window_set_mode(DisplayServer.WINDOW_MODE_FULLSCREEN)
	if DisplayServer.window_get_mode() == DisplayServer.WINDOW_MODE_WINDOWED and first_time:
		DisplayServer.window_set_size(Vector2i(1152, 864))
		first_time = false
