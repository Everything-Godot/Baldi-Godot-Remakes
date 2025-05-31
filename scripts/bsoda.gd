extends Node3D

const ITEM_ID := "BSODA"
const RAY_LENGTH := 1000
const SPEED := 5.0
var sprite_script := load("res://scripts/sprite_rotation.gd")
var spray_texture := load("res://sprites/BSODA_Spray.png")
var exec_id := 0
var school_house
var spray_sprite: Sprite3D
var anim: Animation
var anim_lib: AnimationLibrary
var anim_player: AnimationPlayer
var timer: Timer

func use(executor_id: int):
	exec_id = executor_id
	print("Starting codes")
	school_house = get_node("/root/SchoolHouse")
	print("Creating spray sprite for bsoda")
	spray_sprite = Sprite3D.new()
	spray_sprite.texture = spray_texture
	spray_sprite.name = "spray sprite"
	spray_sprite.position = Vector3(99999, 0, 99999)
	spray_sprite.pixel_size = 0.075
	spray_sprite.set_script(sprite_script)
	school_house.add_child(spray_sprite)
	print("created, result: " + str(spray_sprite))
	var space_state = get_world_3d().direct_space_state
	var cam = get_viewport().get_camera_3d()
	var player = cam.get_parent()
	var mousepos = get_viewport().get_mouse_position()
	var origin = cam.project_ray_origin(mousepos)
	var end = origin + cam.project_ray_normal(mousepos) * RAY_LENGTH
	var query = PhysicsRayQueryParameters3D.create(origin, end)
	query.collide_with_areas = true
	var result = space_state.intersect_ray(query)
	var mv_start: Vector3 = result["normal"]
	mv_start.y = cam.position.y + player.position.y
	var mv_end: Vector3 = result["position"]
	mv_end.x *= 10
	mv_end.z *= 10
	print("ray cast finished, result: " + str(mv_start) + " " + str(mv_end))
	print("Creating audio player")
	print("Creating animation")
	anim = Animation.new()
	anim_lib = AnimationLibrary.new()
	anim_player = AnimationPlayer.new()
	anim_player.name = "AnimationPlayer"
	var move_track = anim.add_track(Animation.TYPE_POSITION_3D)
	anim.length = SPEED
	anim.track_set_path(move_track, spray_sprite.get_path())
	anim.position_track_insert_key(move_track, 0.0, mv_start)
	anim.position_track_insert_key(move_track, SPEED, mv_end)
	anim_lib.add_animation("test", anim)
	anim_player.add_animation_library("test", anim_lib)
	spray_sprite.add_child(anim_player)
	timer = Timer.new()
	timer.name = "Timer For Sprite"
	timer.wait_time = SPEED
	timer.timeout.connect(_on_timer_time_out)
	spray_sprite.add_child(timer)
	print("Done! starts playing")
	timer.start()
	anim_player.play("test/test")

func destory():
	var global_node = get_node("/root/Global")
	var executor_node: Node
	for executor in global_node.get_children():
		if executor.get_meta("item_id") == ITEM_ID:
			if executor.get_meta("executor_id") == exec_id:
				executor_node = executor
				break
	if executor_node == null:
		push_error("An error occour during process of executor")
	else:
		Global.item_use_finished.emit(ITEM_ID, executor_node)

func _on_timer_time_out():
	print("animation finished! removing animation player and others")
	spray_sprite.remove_child(anim_player)
	spray_sprite.remove_child(timer)
	school_house.remove_child(spray_sprite)
	anim_player.queue_free()
	timer.queue_free()
	spray_sprite.queue_free()
	destory()
