extends Node

var first_time : bool = true
var mouse_sensitivity : float
var debug : bool = OS.is_debug_build()
var paused : bool = false
var freelook : bool = false
var unlockedlook : bool = false
var noclip : bool = false
var os_name : String = OS.get_name()
var args : PackedStringArray = OS.get_cmdline_args()

func _process(_delta: float) -> void:
	if Input.is_action_just_pressed("toggle fullscreen") and debug and not paused:
		if DisplayServer.window_get_mode() == DisplayServer.WINDOW_MODE_FULLSCREEN:
			DisplayServer.window_set_mode(DisplayServer.WINDOW_MODE_WINDOWED)
		else:
			DisplayServer.window_set_mode(DisplayServer.WINDOW_MODE_FULLSCREEN)
	if DisplayServer.window_get_mode() == DisplayServer.WINDOW_MODE_WINDOWED and first_time:
		DisplayServer.window_set_size(Vector2i(1152, 864))
		first_time = false
