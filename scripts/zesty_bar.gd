extends Node3D

const ITEM_ID := "Zesty Bar"
var exec_id := 0
var player: Node

func use(executor_id: int):
	exec_id = executor_id
	print("Starting codes")
	Global.limit_stamina = false
	player = get_node("/root").get_child(1).get_child(8)
	player.stamina = 150.0

func _process(_delta: float) -> void:
	if player.stamina <= player.max_stamina:
		destory()

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
