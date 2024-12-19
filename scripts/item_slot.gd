extends Control

@onready var selected : ColorRect = $Selected
@onready var item_name: Label = $"Item Name"
@onready var item_sprites: Control = $"Item Sprites"
var blank_texture = load("res://sprites/Blank.png")
var selected_positions : Array[Vector2] = [
	Vector2(1586, 16), Vector2(1703, 19), Vector2(1810, 18)
]

func _ready() -> void:
	Global.slot_items.resize(3)
	Global.slot_items = ["", "", ""]
	Global.selected_item_slot = 0
	if Global.is_on_android:
		position.y = 125

func _process(delta: float) -> void:
	if Global.selected_item_slot >= 0 and Global.selected_item_slot <= 2:
		selected.position = selected_positions[Global.selected_item_slot]
	else:
		if Global.selected_item_slot < 0:
			Global.selected_item_slot = 2
		else:
			Global.selected_item_slot = 0
	var currect_item = get_currect_item_info(Global.selected_item_slot)
	if currect_item == "":
		item_name.text = "Nothing"
	elif currect_item == "Quarter":
		item_name.text = "Quarter"
	else:
		item_name.text = ""
	var i = 0
	for item in Global.slot_items:
		var sprite = get_sprite_for_item(item)
		if sprite == null:
			item_sprites.item_sprites[i].texture = blank_texture
		else:
			item_sprites.item_sprites[i].texture = sprite
		i += 1

func get_currect_item_info(currect_item_slot : int) -> String:
	return Global.slot_items[currect_item_slot]

func get_sprite_for_item(item : String) -> Resource:
	for sprite_info in Global.item_sprites:
		if sprite_info[0] == item:
			return sprite_info[1]
	return null

func _input(event: InputEvent) -> void:
	if event is InputEventMouseButton:
		if not Global.paused:
			if event.is_pressed():
				if event.button_index == MOUSE_BUTTON_WHEEL_UP:
					Global.selected_item_slot -= 1
					print("Selected item slot "+str(Global.selected_item_slot))
				if event.button_index == MOUSE_BUTTON_WHEEL_DOWN:
					Global.selected_item_slot += 1
					print("Selected item slot "+str(Global.selected_item_slot))
