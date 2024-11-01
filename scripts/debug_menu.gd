extends Control

@onready var freelook = $Panel/Freelook
@onready var noclip = $Panel/Noclip
@onready var unlockedlook = $"Panel/3DLook"
@onready var freelook_button = $Panel/Freelook/Button
@onready var noclip_button = $Panel/Noclip/Button
@onready var unlockedlook_button = $"Panel/3DLook/Button"

func _ready() -> void:
	if Global.os_name == "Android":
		freelook_button.visible = true
		noclip_button.visible = true
		unlockedlook_button.visible = true
	else:
		freelook_button.visible = false
		noclip_button.visible = false
		unlockedlook_button.visible = false
	if Global.debug:
		visible = true
	else:
		visible = false

func _process(_delta: float) -> void:
	if Global.freelook:
		freelook.text = "[center]Freelook    [color=green]ON[/color]"
	else:
		freelook.text = "[center]Freelook    [color=red]OFF[/color]"
	if Global.noclip:
		noclip.text = "[center]Noclip    [color=green]ON[/color]"
	else:
		noclip.text = "[center]Noclip    [color=red]OFF[/color]"
	if Global.unlockedlook:
		unlockedlook.text = "[center]3DLook    [color=green]ON[/color]"
	else:
		unlockedlook.text = "[center]3DLook    [color=red]OFF[/color]"
