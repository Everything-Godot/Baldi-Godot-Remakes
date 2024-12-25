extends Node3D

const RAY_LENGTH = 1000
const SPEED = 20.0
var spray_texture = load("res://sprites/BSODA_Spray.png")
#var spray_sound = load("res://sounds/")

func use():
	print("Starting codes")
	var school_house = get_node("/root/SchoolHouse")
	print("Creating spray sprite for bsoda")
	var spray_sprite : Sprite3D = Sprite3D.new()
	spray_sprite.texture = spray_texture
	spray_sprite.name = "spray sprite"
	spray_sprite.position = Vector3(99999, 0, 99999)
	spray_sprite.pixel_size = 0.05
	school_house.add_child(spray_sprite)
	print("created, result: "+str(spray_sprite))
	var space_state = get_world_3d().direct_space_state
	var cam = get_viewport().get_camera_3d()
	var player = cam.get_parent()
	var mousepos = get_viewport().get_mouse_position()
	var origin = cam.project_ray_origin(mousepos)
	var end = origin + cam.project_ray_normal(mousepos) * RAY_LENGTH
	var query = PhysicsRayQueryParameters3D.create(origin, end)
	query.collide_with_areas = true
	var result = space_state.intersect_ray(query)
	var mv_start : Vector3 = result["normal"]
	mv_start.y = cam.position.y + player.position.y
	var mv_end : Vector3 = result["position"]
	mv_end.x *= 50
	mv_end.z *= 50
	print("ray cast finished, result: " + str(mv_start) + " " + str(mv_end))
	print("Creating audio player")
	#var audio : AudioStreamPlayer = AudioStreamPlayer.new()
	#audio.stream = spray_sound
	#school_house.add_child(audio)
	print("Creating animation")
	var anim : Animation = Animation.new()
	var anim_lib : AnimationLibrary = AnimationLibrary.new()
	var anim_player : AnimationPlayer = AnimationPlayer.new()
	var move_track = anim.add_track(Animation.TYPE_POSITION_3D)
	anim.length = SPEED
	anim.track_set_path(move_track, spray_sprite.get_path())
	anim.position_track_insert_key(move_track, 0.0, mv_start)
	anim.position_track_insert_key(move_track, SPEED, mv_end)
	anim_lib.add_animation("test", anim)
	anim_player.add_animation_library("test", anim_lib)
	spray_sprite.add_child(anim_player)
	print("Done! starts playing")
	anim_player.play("test/test")
	#audio.play()
	await anim_player.animation_finished
	print("done, removing player and others")
	spray_sprite.remove_child(anim_player)
	school_house.remove_child(spray_sprite)
	#school_house.remove_child(audio)
	anim.free()
	anim_lib.free()
	anim_player.queue_free()
	#audio.queue_free()
	spray_sprite.queue_free()
