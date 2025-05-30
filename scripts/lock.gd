extends Node3D

const ITEM_ID = "Lock"
var exec_id : int = 0

func use(executor_id: int):
	exec_id = executor_id
	print("Starting codes")

func destory():
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

func _physics_process(_delta: float) -> void:
	var space_state = get_world_3d().direct_space_state
	var cam : Camera3D = get_node("/root").get_child(1).find_child("Player").get_child(1)
	var mousepos = get_viewport().get_mouse_position()

	var origin = cam.project_ray_origin(mousepos)
	var end = origin + cam.project_ray_normal(mousepos) * 2
	var query = PhysicsRayQueryParameters3D.create(origin, end)
	query.collide_with_areas = true

	var result = space_state.intersect_ray(query)
	if "collider" in result:
		var node = result["collider"]
		if node.get_parent().name == "Swing Door":
			print("Found swing door, locking...")
			node.get_parent().set_meta("Locked", true)
			print("Done!")
			destory()
		else:
			print("Cannot found a swing door. Stopping...")
			set_meta("should_remove_item", false)
			destory()
	else:
		print("Cannot found a swing door. Stopping...")
		set_meta("should_remove_item", false)
		destory()
