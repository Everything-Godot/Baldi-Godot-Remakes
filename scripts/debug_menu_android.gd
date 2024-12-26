extends Control

@onready var freelook = $Panel/Freelook
@onready var noclip = $Panel/Noclip
@onready var unlockedlook = $"Panel/3DLook"

func _ready() -> void:
	if Global.is_on_android:
		pass
	else:
		pass
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

func _on_d_look_btn_pressed() -> void:
	Input.action_press("3dlook")
	Input.action_release("3dlook")

func _on_noclip_btn_pressed() -> void:
	Input.action_press("noclip")
	Input.action_release("noclip")

func _on_freelook_btn_pressed() -> void:
	Input.action_press("freelook")
	Input.action_release("freelook")

func _on_add_notebook_btn_pressed() -> void:
	Input.action_press("Add Notebook")
	Input.action_release("Add Notebook")

func _on_remove_notebook_btn_pressed() -> void:
	Input.action_press("Remove Notebook")
	Input.action_release("Remove Notebook")
