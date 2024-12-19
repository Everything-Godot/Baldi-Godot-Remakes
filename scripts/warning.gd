extends Node2D

var title_screen = preload("res://scenes/places/title_screen.tscn")
var yctp = preload("res://scenes/places/yctp.tscn")
var yctp_test : bool = false

func _process(_delta: float) -> void:
	if Input.is_anything_pressed():
		for arg in Global.args:
			if arg == "--yctp":
				yctp_test = true
				break
		if yctp_test:
			Global.already_wrong = false
			get_tree().change_scene_to_packed(yctp)
		else:
			get_tree().change_scene_to_packed(title_screen)
