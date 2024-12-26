extends CharacterBody3D

@onready var camera = $"Camera3D"
@onready var collision = $CollisionShape3D
@onready var area3d = $Camera3D/Area3D
@export_category("player")
@export var speed : float = 5.0
@export var gravity : float = 20.0
@export var jump_speed : float = 5.0
var yctp : PackedScene = load("res://scenes/places/yctp.tscn")
var yctp_node : Node
var jumping := false
var movement_vector := Vector2.ZERO
var last_floor := false
var parent : Node
var temp_bool
var temp_bool2
var last_camera_position
var last_camera_rotation
var check_yctp : bool = false

func _ready() -> void:
	if Global.is_on_android:
		Input.mouse_mode = Input.MOUSE_MODE_VISIBLE
	else:
		Input.mouse_mode = Input.MOUSE_MODE_CAPTURED
	area3d.monitoring = false

func _process(_delta: float) -> void:
	if Global.debug:
		get_tree().debug_collisions_hint = true
		get_tree().debug_navigation_hint = true
		get_tree().debug_paths_hint = true
		if not Global.paused:
			if Input.is_action_just_pressed("Add Notebook"):
				Global.notebooks += 1
			if Input.is_action_just_pressed("Remove Notebook"):
				Global.notebooks -= 1
	if not Global.paused:
		if Input.is_action_pressed("interact"):
			area3d.monitoring = true
			await get_tree().create_timer(0.5).timeout
			area3d.monitoring = false
		if Input.is_action_just_pressed("use"):
			print("Decteced use command, start checking item slot")
			if Global.slot_items[Global.selected_item_slot] == "":
				push_warning("Item slot is empty, aboring")
			else:
				print("Calling use function for item")
				for i in Global.item_codes:
					if i[0] == Global.slot_items[Global.selected_item_slot]:
						if i[1] == null:
							push_error("No script found for this item, please contact developer for help!")
							Global.slot_items[Global.selected_item_slot] = ""
							break
						var executor_id : int = 0
						var has_all_id : bool = false
						var global_node = get_node("/root/Global")
						if not global_node.get_children().is_empty():
							while not has_all_id:
								var iab : int = 0
								for child in global_node.get_children():
									if child.get_meta("item_id") == i[0]:
										if child.get_meta("executor_id") == executor_id:
											if not executor_id == 9:
												executor_id += 1
											else:
												has_all_id = true
											iab += 1
								if iab == global_node.get_children().size():
									break
						var node : Node3D = Node3D.new()
						node.name = str(i[0]) + " Item Script Executer #"+str(executor_id)
						node.set_meta("item_id", i[0])
						node.set_meta("executor_id", executor_id)
						node.set_script(i[1])
						global_node.add_child(node)
						node.use(executor_id)
						Global.slot_items[Global.selected_item_slot] = ""
						Global.item_use_finished.connect(_on_item_use_finishes)
	if check_yctp:
		if not Global.in_yctp:
			check_yctp = false
			Global.notebooks += 1
	if Global.paused and Global.in_yctp:
		Input.mouse_mode = Input.MOUSE_MODE_VISIBLE
	else:
		if Global.is_on_android:
			Input.mouse_mode = Input.MOUSE_MODE_VISIBLE
		else:
			Input.mouse_mode = Input.MOUSE_MODE_CAPTURED
		if yctp_node != null:
			if camera.has_node(yctp_node.get_path()):
				camera.remove_child(yctp_node)
				yctp_node.free()

func _physics_process(delta: float) -> void:
	if not Global.paused:
		if not Global.freelook:
			velocity.y += -gravity * delta
			var vy = velocity.y
			velocity.y = 0
			var inputDir = Input.get_vector("left", "right", "forward", "backward")
			var relativeDir : Vector3
			if Global.look_back:
				relativeDir = Vector3(inputDir.x, 0.0, inputDir.y).rotated(Vector3.UP, -camera.rotation.y)
			else:
				relativeDir = Vector3(inputDir.x, 0.0, inputDir.y).rotated(Vector3.UP, camera.rotation.y)
			velocity = lerp(velocity, relativeDir * speed, speed * delta)
			if not Global.noclip:
				velocity.y = vy
				if is_on_floor() and not last_floor:
					jumping = false
				last_floor = is_on_floor()
				if is_on_floor() and Input.is_action_just_pressed("jump"):
					velocity.y = jump_speed
					jumping = true
			else:
				jumping = true
		else:
			velocity = Vector3(0, 0, 0)
		move_and_slide()
		if Global.debug:
			if Input.is_action_just_pressed("noclip"):
				if Global.noclip:
					collision.disabled = false
					gravity = 20.0
					Global.unlockedlook = temp_bool
					temp_bool = null
				else:
					collision.disabled = true
					gravity = 0.0
					temp_bool = Global.unlockedlook
					Global.unlockedlook = true
				if not Global.unlockedlook:
					camera.rotation.x = 0
				Global.noclip = !Global.noclip
			if Global.noclip:
				if Input.is_action_pressed("jump"):
					position.y += 0.1
				if Input.is_action_pressed("down"):
					position.y -= 0.1
	else:
		area3d.monitoring = false

func _on_area_3d_area_entered(area:Area3D) -> void:
	parent = area.get_parent()
	if not Global.paused:
		if not Global.freelook:
			if parent.has_meta("pickup"):
				print("Dected pickup, getting informations.")
				parent = parent.get_parent()
				for i in Global.items:
					if parent.name == i:
						parent.visible = false
						print("Checking currect slot")
						if Global.slot_items[Global.selected_item_slot] == "":
							print("currect slot is empty, setting item")
							Global.slot_items[Global.selected_item_slot] = i
							print("succed to place item "+str(i)+" into slot "+str(Global.selected_item_slot))
						else:
							print("currect slot is not empty, checking other slots")
							var all_filled : bool = true
							for item in Global.slot_items:
								if item == "":
									print("find other empty slot")
									all_filled = false
									print_rich("[color=green]all_filled = " + str(all_filled) + "[/color]")
									break
							if all_filled:
								print_rich("[color=red]all_filled = " + str(all_filled) + "[/color]")
								print("none one of slot is empty, replacing currect slot")
								Global.slot_items[Global.selected_item_slot] = i
								print("succed to place item "+str(i)+" into slot "+str(Global.selected_item_slot))
							else:
								if Global.slot_items[0] == "":
									Global.slot_items[0] = i
									print("succed to place item "+str(i)+" into slot 0")
								elif Global.slot_items[1] == "":
									Global.slot_items[1] = i
									print("succed to place item "+str(i)+" into slot 1")
								elif Global.slot_items[2] == "":
									Global.slot_items[2] = i
									print("succed to place item "+str(i)+" into slot 2")
						break
			elif parent.has_meta("is_swing_door"):
				if not parent.get_meta("is_swing_door"):
					if parent.has_meta("opened"):
						if not parent.get_meta("opened"):
							parent.set_meta("opened", true)
			elif parent.name == "Notebook":
				yctp_node = yctp.instantiate()
				camera.add_child(yctp_node)
				Global.in_yctp = true
				Global.paused = true
				check_yctp = true

func _on_area_3d_area_exited(area: Area3D) -> void:
	parent = area.get_parent()

func _on_body_entered(area: Area3D) -> void:
	parent = area.get_parent()
	if not Global.paused:
		if not Global.freelook:
			if parent.has_meta("is_swing_door"):
				if parent.get_meta("is_swing_door"):
					if parent.has_meta("opened"):
						if not parent.get_meta("opened"):
							parent.set_meta("opened", true)

func _on_body_exited(area: Area3D) -> void:
	parent = area.get_parent()

func _on_item_use_finishes(item_id : String, executor_node : Node) -> void:
	if executor_node.get_meta("item_id") == item_id:
		executor_node.get_parent().remove_child(executor_node)
		executor_node.queue_free()
	else:
		push_error("ERROR Code 01")
