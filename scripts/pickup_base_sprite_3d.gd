@tool
extends Sprite3D

var anim_player = AnimationPlayer.new()
var anim = Animation.new()
var anim_library = AnimationLibrary.new()

func _ready() -> void:
	print("start generating animation for pickup sprite 3d")
	print("generating animation")
	anim.length = 1.2
	var anim_track = anim.add_track(Animation.TYPE_VALUE)
	var node_path : NodePath = get_path()
	anim.track_set_path(anim_track, "%s:position:y" % node_path)
	var default_y_position = position.y
	anim.track_insert_key(anim_track, 0.0, default_y_position)
	anim.track_insert_key(anim_track, 0.3, default_y_position+0.3)
	anim.track_insert_key(anim_track, 0.6, default_y_position)
	anim.track_insert_key(anim_track, 0.9, default_y_position-0.3)
	anim.track_insert_key(anim_track, 1.2, default_y_position)
	anim.resource_name = "Animation"
	print("animation generated! result: "+str(anim))
	print("generating animation library")
	anim_library.add_animation("Animation", anim)
	if anim_library.has_animation("Animation"):
		print("successfully add animation into animation library!")
	else:
		push_error("failed to add animation into animation library, please contact the developer if you believe this is a bug.")
	print("animation library generated! result: "+str(anim_library))
	anim_player.connect("animation_finished", _on_finish)
	anim_player.add_animation_library("library", anim_library)
	print("animation player generated! result: "+str(anim_player))
	add_child(anim_player)
	print("added animation player to notebook scene")
	if anim_player.has_animation_library("library"):
		if anim_player.has_animation("library/Animation"):
			print("try to play animation")
			anim_player.play("library/Animation")
			if anim_player.is_playing():
				print("succeed!")
			else:
				push_error("failed to play animation for notebook, please contact the developer if you believe this is a bug.")
		else:
			push_error("failed to load animation into animation player, please contact the developer if you believe this is a bug.")
	else:
		push_error("failed to load animation library into animation player, please contact the developer if you believe this is a bug.")

func _on_finish(anim_name: StringName) -> void:
	if anim_name == "library/Animation":
		anim_player.play("library/Animation")
