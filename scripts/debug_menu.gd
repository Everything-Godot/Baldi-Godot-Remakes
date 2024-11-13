extends Control

@onready var freelook = $Panel/Freelook
@onready var noclip = $Panel/Noclip
@onready var unlockedlook = $"Panel/3DLook"

func _ready() -> void:
	if Global.is_on_android:
		visible = false
	else:
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
