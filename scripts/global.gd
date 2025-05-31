extends Node

signal item_use_finished(item_id: String, executor_node: Node)
var first_time := true
var mouse_sensitivity: float
var debug := OS.is_debug_build()
var paused := false
var freelook := false
var unlockedlook := false
var noclip := false
var look_back := false
var running := false
var limit_stamina := true
var os_name := OS.get_name()
var args := OS.get_cmdline_args()
var is_on_android := false
var in_yctp := false
var already_wrong := false
var notebooks := 0
var total_notebooks := 3
var yctp_refreshed := false
var selected_item_slot := 0
var slot_items: Array[String]
var items := [
	"BSODA", "Key", "Lock", "Quarter", "Tape", "Zesty Bar"
]
#Need to match on all three stuff because hard coded :)
var item_sprites := [
	["BSODA", load("res://sprites/BSODA.png")],
	["Key", load("res://sprites/Key.png")],
	["Lock", load("res://sprites/YellowDoorLock.png")],
	["Quarter", load("res://sprites/Quarter.png")],
	["Tape", load("res://sprites/Tape.png")],
	["Zesty Bar", load("res://sprites/EnergyFlavoredZestyBar.png")]
]
var item_names := [
	["BSODA", "BSODA"],
	["Key", "Principal's Keys"],
	["Lock", "Swinging Door Lock"],
	["Quarter", "Quarter"],
	["Tape", "Baldi's Least Favorite Tape"],
	["Zesty Bar", "Energy Flavored Zesty Bar"]
]
var item_codes := [
	["BSODA", load("res://scripts/bsoda.gd")],
	["Key", null],
	["Lock", load("res://scripts/lock.gd")],
	["Quarter", null],
	["Tape", null],
	["Zesty Bar", load("res://scripts/zesty_bar.gd")]
]

func _ready() -> void:
	print("Running on: " + os_name)
	print("Start with argument: " + str(args))
	if os_name == "Android":
		is_on_android = true
	elif os_name == "Web":
		var ua: String = str(JavaScriptBridge.get_interface("navigator").userAgent)
		print("Get User Agent: " + ua)
		if ua.find("Android") != -1:
			is_on_android = true
		else:
			is_on_android = false
	else:
		for arg in args:
			if arg == "--android-debug":
				print("Find debug argument: " + str(arg))
				print("Setting game as android build.")
				is_on_android = true
				ProjectSettings.set_setting("input_devices/pointing/emulate_touch_from_mouse", true)
				DisplayServer.window_set_title("Android feature test")
			elif arg == "--yctp":
				print("Find debug argument: " + str(arg))
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
