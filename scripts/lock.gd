extends Node3D

const ITEM_ID = "Lock"
var exec_id : int = 0

func use(executor_id: int):
	exec_id = executor_id
	print("Starting codes")
	get_node("/root").get_child(1).get_child(4).get_child(1).set_meta("Locked", true)
	print("Done!")

func _on_timer_time_out():
	print("animation finished! removing animation player and others")
	var global_node = get_node("/root/Global")
	var executor_node : Node
	for executor in global_node.get_children():
		if executor.get_meta("item_id") == ITEM_ID:
			if executor.get_meta("executor_id") == exec_id:
				executor_node = executor
				break
	if executor_node == null:
		push_error("An error occour during process of executor")
	else:
		Global.item_use_finished.emit(ITEM_ID, executor_node)
