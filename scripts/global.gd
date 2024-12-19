extends Node

var first_time : bool = true
var mouse_sensitivity : float
var debug : bool = OS.is_debug_build()
var paused : bool = false
var freelook : bool = false
var unlockedlook : bool = false
var noclip : bool = false
var look_back : bool = false
var running : bool = false
var os_name : String = OS.get_name()
var args : PackedStringArray = OS.get_cmdline_args()
var is_on_android : bool = false
var in_yctp : bool = false
var already_wrong : bool = false
var notebooks : int = 0
var total_notebooks : int = 3
var yctp_refreshed : bool = false
var selected_item_slot : int = 0
var slot_items : Array[String]
var items : Array[String] = [
	"BSODA", "Key", "Lock", "Quarter", "Tape", "Zesty Bar"
]
var item_sprites = [
	["BSODA", load("res://sprites/BSODA.png")],
	["Key", load("res://sprites/Key.png")],
	["Lock", load("res://sprites/YellowDoorLock.png")],
	["Quarter", load("res://sprites/Quarter.png")],
	["Tape", load("res://sprites/Tape.png")],
	["Zesty Bar", load("res://sprites/EnergyFlavoredZestyBar.png")]
]
var item_names = [
	["BSODA", "BSODA"],
	["Key", "Principal's Keys"],
	["Lock", "Swinging Door Lock"],
	["Quarter", "Quarter"],
	["Tape", "Baldi's Least Favorite Tape"],
	["Zesty Bar", "Energy Flavored Zesty Bar"]
]

func _ready() -> void:
	print("Running on: "+os_name)
	print("Start with argument: "+str(args))
	if os_name == "Android":
		is_on_android = true
	elif os_name == "Web":
		var ua : String = str(JavaScriptBridge.get_interface("navigator").userAgent)
		print("Get User Agent: "+ua)
		if ua.find("Android") != -1:
			is_on_android = true
		else:
			is_on_android = false
	else:
		for arg in args:
			if arg == "--android-debug":
				print("Find debug argument: "+str(arg))
				print("Setting game as android build.")
				is_on_android = true
				ProjectSettings.set_setting("input_devices/pointing/emulate_touch_from_mouse", true)
				DisplayServer.window_set_title("Android feature test")
			elif arg == "--yctp":
				print("Find debug argument: "+str(arg))
				print("Setting game to yctp.")
				DisplayServer.window_set_title("YCTP test")

func _process(_delta: float) -> void:
	if not is_on_android:
		if Input.is_action_just_pressed("toggle fullscreen") and debug and not paused:
			if DisplayServer.window_get_mode() == DisplayServer.WINDOW_MODE_FULLSCREEN:
				DisplayServer.window_set_mode(DisplayServer.WINDOW_MODE_WINDOWED)
			else:
				DisplayServer.window_set_mode(DisplayServer.WINDOW_MODE_FULLSCREEN)
		if DisplayServer.window_get_mode() == DisplayServer.WINDOW_MODE_WINDOWED and first_time:
			DisplayServer.window_set_size(Vector2i(1152, 864))
			first_time = false
	else:
		DisplayServer.window_set_mode(DisplayServer.WINDOW_MODE_FULLSCREEN)
