extends Control

@onready var this : Control = $"."
@onready var input_box : TextEdit = $EmptyBox
@onready var placeholder : Label = $Placeholder
@onready var question : Label = $question
@onready var unanswerable_question : Control = $nonquestion
@onready var question_num : Control = $Question_num
@onready var question_marks : Control = $Question_marks
@onready var music : AudioStreamPlayer2D = $music
@onready var baldi_audio : AudioStreamPlayer2D = $baldi_audio
@onready var baldi_talks : AnimatedSprite2D = $Baldi
var empty_sound = load("res://sounds/delay.wav")
var intro_sounds : Array[Resource] = [
	load("res://sounds/BAL_Math_Intro.wav"),
	load("res://sounds/BAL_General_HowTo.wav")
]
var question_sounds : Array[Resource] = [
	load("res://sounds/BAL_General_Problem1.wav"),
	load("res://sounds/BAL_General_Problem2.wav"),
	load("res://sounds/BAL_General_Problem3.wav")
]
var number_sounds : Array[Resource] = [
	load("res://sounds/BAL_Math_0.wav"),
	load("res://sounds/BAL_Math_1.wav"),
	load("res://sounds/BAL_Math_2.wav"),
	load("res://sounds/BAL_Math_3.wav"),
	load("res://sounds/BAL_Math_4.wav"),
	load("res://sounds/BAL_Math_5.wav"),
	load("res://sounds/BAL_Math_6.wav"),
	load("res://sounds/BAL_Math_7.wav"),
	load("res://sounds/BAL_Math_8.wav"),
	load("res://sounds/BAL_Math_9.wav"),
	load("res://sounds/BAL_Screech.wav")
]
var caculate_sounds : Array[Resource] = [
	load("res://sounds/BAL_Math_Divided.wav"),
	load("res://sounds/BAL_Math_Plus.wav"),
	load("res://sounds/BAL_Math_Minus.wav"),
	load("res://sounds/BAL_Math_Times.wav"),
	load("res://sounds/BAL_Math_Equals.wav")
]
var caculate_string : Array[String] = [
	"/", "+", "-", "*"
]
var praise_sounds : Array[Resource] = [
	load("res://sounds/BAL_Praise1.wav"),
	load("res://sounds/BAL_Praise2.wav"),
	load("res://sounds/BAL_Praise3.wav"),
	load("res://sounds/BAL_Praise4.wav"),
	load("res://sounds/BAL_Praise5.wav")
]
var equals : int = 4
var problem : int = -1
var nums : Array[int] = [0, 0, 0]
var answer : int
var correct : bool = false

func _ready() -> void:
	baldi_talks.speed_scale = 1
	input_box.autowrap_mode = TextServer.AUTOWRAP_OFF
	for child in question_marks.get_children():
		child.visible = false
	for child in question_num.get_children():
		child.visible = false
	question_marks.visible = true
	question_num.visible = true
	question.visible = false
	unanswerable_question.visible = false
	if Global.already_wrong:
		baldi_talks.play("empty")
	else:
		baldi_talks.play("default")
	problem += 1
	generate_questions()
	input_box.grab_focus()
	play_intro()

func generate_questions() -> void:
	print("Generating question.")
	var space = " "
	var text = "= ?"
	if Global.notebooks < 1:
		if problem < 3:
			nums[0] = randi_range(0, 9)
			nums[1] = randi_range(1, 2)
			if nums[1] == 0:
				nums[2] = randi_range(1, 9)
			else:
				nums[2] = randi_range(0, 9)
			print("Generated nums: " + str(nums))
			if nums[1] == 0:
				answer = nums[0] / nums[2]
			elif nums[1] == 1:
				answer = nums[0] + nums[2]
			elif nums[1] == 2:
				answer = nums[0] - nums[2]
			elif nums[1] == 3:
				answer = nums[0] * nums[2]
			else:
				answer = 0
			print("Question answer: " + str(answer))
			question.text = str(nums[0]) + space + caculate_string[nums[1]] + space + str(nums[2]) + space + text
			question.visible = true
		else:
			print("Skip because last question")
			question.position = question_num.get_child(0).position
			if Global.already_wrong:
				question.text = "You failed math?! Why?"
			else:
				question.text = "Wow, you exit!"
	else:
		if problem < 2:
			nums[0] = randi_range(0, 9)
			nums[1] = randi_range(1, 2)
			if nums[1] == 0:
				nums[2] = randi_range(1, 9)
			else:
				nums[2] = randi_range(0, 9)
			print("Generated nums: " + str(nums))
			if nums[1] == 0:
				answer = nums[0] / nums[2]
			elif nums[1] == 1:
				answer = nums[0] + nums[2]
			elif nums[1] == 2:
				answer = nums[0] - nums[2]
			elif nums[1] == 3:
				answer = nums[0] * nums[2]
			else:
				answer = 0
			print("Question answer: " + str(answer))
			question.text = str(nums[0]) + space + caculate_string[nums[1]] + space + str(nums[2]) + space + text
			question.visible = true
		elif problem == 2:
			nums[0] = 0
			nums[1] = 0
			nums[2] = 0
			print("no answer for this quesition")
			var node : Node = unanswerable_question.get_child(3)
			node.visible = false
			question.text = ""
			question.visible = false
			unanswerable_question.visible = true
		else:
			print("Skip because last question")
			var node : Node = unanswerable_question.get_child(2)
			node.visible = false
			node = unanswerable_question.get_child(3)
			node.visible = true
			question.position = question_num.get_child(0).position
			question.text = "You failed math?! Why?"
			question.visible = true
	print("Generated question!")

func play_intro() -> void:
	if Global.notebooks < 1:
		for sound in intro_sounds:
			baldi_audio.stream = sound
			baldi_audio.play()
			await baldi_audio.finished
	if Global.already_wrong:
		music.stream = empty_sound
	read_question()

func read_question() -> void:
	if not Global.already_wrong:
		if problem < 2:
			baldi_audio.stream = question_sounds[problem]
			baldi_audio.play()
			await baldi_audio.finished
			await get_tree().create_timer(0.1).timeout
			baldi_audio.stream = number_sounds[nums[0]]
			baldi_audio.play()
			await baldi_audio.finished
			await get_tree().create_timer(0.1).timeout
			baldi_audio.stream = caculate_sounds[nums[1]]
			baldi_audio.play()
			await baldi_audio.finished
			await get_tree().create_timer(0.1).timeout
			baldi_audio.stream = number_sounds[nums[2]]
			baldi_audio.play()
			await baldi_audio.finished
			await get_tree().create_timer(0.1).timeout
			baldi_audio.stream = caculate_sounds[4]
			baldi_audio.play()
			await baldi_audio.finished
			await get_tree().create_timer(0.1).timeout
		elif problem == 2:
			if Global.notebooks >= 1:
				baldi_audio.stream = question_sounds[problem]
				baldi_audio.play()
				await baldi_audio.finished
				await get_tree().create_timer(0.1).timeout
				baldi_audio.stream = number_sounds[10]
				baldi_audio.play()
				await baldi_audio.finished
				await get_tree().create_timer(0.1).timeout
				baldi_audio.stream = caculate_sounds[randi_range(0, 3)]
				baldi_audio.play()
				await baldi_audio.finished
				await get_tree().create_timer(0.1).timeout
				baldi_audio.stream = number_sounds[10]
				baldi_audio.play()
				await baldi_audio.finished
				await get_tree().create_timer(0.1).timeout
				baldi_audio.stream = caculate_sounds[randi_range(0, 3)]
				baldi_audio.play()
				await baldi_audio.finished
				await get_tree().create_timer(0.1).timeout
				baldi_audio.stream = number_sounds[10]
				baldi_audio.play()
				await baldi_audio.finished
				await get_tree().create_timer(0.1).timeout
				baldi_audio.stream = caculate_sounds[4]
				baldi_audio.play()
				await baldi_audio.finished
				await get_tree().create_timer(0.1).timeout
			else:
				baldi_audio.stream = question_sounds[problem]
				baldi_audio.play()
				await baldi_audio.finished
				await get_tree().create_timer(0.1).timeout
				baldi_audio.stream = number_sounds[nums[0]]
				baldi_audio.play()
				await baldi_audio.finished
				await get_tree().create_timer(0.1).timeout
				baldi_audio.stream = caculate_sounds[nums[1]]
				baldi_audio.play()
				await baldi_audio.finished
				await get_tree().create_timer(0.1).timeout
				baldi_audio.stream = number_sounds[nums[2]]
				baldi_audio.play()
				await baldi_audio.finished
				await get_tree().create_timer(0.1).timeout
				baldi_audio.stream = caculate_sounds[4]
				baldi_audio.play()
				await baldi_audio.finished
				await get_tree().create_timer(0.1).timeout
	baldi_audio.stream = empty_sound

func _process(_delta: float) -> void:
	if problem == 0:
		for child in question_num.get_children():
			if child.name == "Problem 1":
				child.visible = true
			else:
				child.visible = false
	elif problem == 1:
		for child in question_num.get_children():
			if child.name == "Problem 2":
				child.visible = true
			else:
				child.visible = false
	elif problem == 2:
		for child in question_num.get_children():
			if child.name == "Problem 3":
				child.visible = true
			else:
				child.visible = false
	else:
		for child in question_num.get_children():
			child.visible = false
	if not Global.already_wrong and not problem >= 3:
		if baldi_audio.playing and not baldi_audio.stream == empty_sound:
			baldi_talks.play("talking")
		else:
			baldi_talks.play("default")
	if not Global.already_wrong and problem >= 3:
		number_sounds = [empty_sound, empty_sound, empty_sound, empty_sound, empty_sound, empty_sound, empty_sound, empty_sound, empty_sound, empty_sound]
		question_sounds = [empty_sound, empty_sound, empty_sound]
		caculate_sounds = [empty_sound, empty_sound, empty_sound, empty_sound, empty_sound]
		if baldi_audio.stream == empty_sound:
			baldi_talks.play("default")
		else:
			await baldi_audio.finished
			baldi_audio.stream = empty_sound
	if Global.already_wrong:
		number_sounds = [empty_sound, empty_sound, empty_sound, empty_sound, empty_sound, empty_sound, empty_sound, empty_sound, empty_sound, empty_sound]
		question_sounds = [empty_sound, empty_sound, empty_sound]
		caculate_sounds = [empty_sound, empty_sound, empty_sound, empty_sound, empty_sound]
	if input_box.text == "":
		placeholder.visible = true
	else:
		placeholder.visible = false
	var letters : PackedStringArray = ["a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "[",
		"]", "{", "}", ";", ":", "'", '"', "\\", "|", ",", "<", ".", ">", "/", "?", "_", "=", "+", "*", "/", "\n", " "]
	var find_letter : int
	var is_enter : bool = false
	for i in letters:
		find_letter = input_box.text.to_lower().find(i)
		if find_letter != -1:
			if i == "\n":
				is_enter = true
			break
	var old_input_box_text : String = ""
	if find_letter != -1:
		var text_length : int = len(input_box.text)
		var last_position : int = input_box.get_caret_column()
		if is_enter:
			old_input_box_text = input_box.text
		input_box.text = input_box.text.substr(0, find_letter) + input_box.text.substr(find_letter+1, text_length)
		input_box.set_caret_column(last_position - 1)
	if is_enter:
		print("old input box text: "+old_input_box_text.replace("\n", "\\n"))
		print("substr result: "+old_input_box_text.substr(find_letter, find_letter+1).replace("\n", "\\n"))
	if Input.is_action_just_pressed("confim_answer") or old_input_box_text.substr(find_letter, find_letter+1) == "\n":
		handel_answer()
	if problem >= 3:
		input_box.visible = false
		placeholder.text = ""
		if baldi_audio.stream != empty_sound and baldi_audio.playing:
			await baldi_audio.finished
		if baldi_talks.is_playing() and baldi_talks.animation != "default" and baldi_talks.animation != "angry default" and baldi_talks.animation != "empty":
			await baldi_talks.animation_finished
		await get_tree().create_timer(2).timeout
		Global.in_yctp = false
		Global.paused = false

func read_praise() -> void:
	if not Global.already_wrong and correct:
		var ran = randi_range(0, 4)
		baldi_audio.stream = praise_sounds[ran]
		baldi_audio.play()
		await baldi_audio.finished
		await get_tree().create_timer(0.1).timeout
		if not problem >= 3:
			read_question()
	elif not Global.already_wrong and not correct:
		Global.already_wrong = true

func handel_answer() -> void:
	baldi_audio.stop()
	var temp1 = number_sounds
	var temp2 = question_sounds
	var temp3 = caculate_sounds
	number_sounds = [empty_sound, empty_sound, empty_sound, empty_sound, empty_sound, empty_sound, empty_sound, empty_sound, empty_sound, empty_sound, empty_sound]
	question_sounds = [empty_sound, empty_sound, empty_sound]
	caculate_sounds = [empty_sound, empty_sound, empty_sound, empty_sound, empty_sound]
	baldi_audio.stream = load("res://sounds/delay.wav")
	read_question()
	if Global.notebooks >= 1 and problem == 3:
		correct = false
	else:
		if int(input_box.text) == answer:
			correct = true
		else:
			correct = false
	input_box.text = ""
	if correct:
		if problem == 0:
			for child in question_marks.get_children():
				if child.name == "Check1":
					child.visible = true
					break
		elif problem == 1:
			for child in question_marks.get_children():
				if child.name == "Check2":
					child.visible = true
					break
		elif problem == 2:
			for child in question_marks.get_children():
				if child.name == "Check3":
					child.visible = true
					break
	else:
		if problem == 0:
			for child in question_marks.get_children():
				if child.name == "X1":
					child.visible = true
					break
		elif problem == 1:
			for child in question_marks.get_children():
				if child.name == "X2":
					child.visible = true
					break
		elif problem == 2:
			for child in question_marks.get_children():
				if child.name == "X3":
					child.visible = true
					break
	problem += 1
	if not problem >= 4:
		if not correct and not Global.already_wrong:
			Global.already_wrong = true
			music.stop()
			music.stream = load("res://sounds/mus_hang.wav")
			music.play()
			baldi_audio.stop()
			baldi_talks.speed_scale = 0.3
			baldi_talks.play("angry")
		else:
			read_praise()
	generate_questions()
	await get_tree().create_timer(1).timeout
	number_sounds = temp1
	question_sounds = temp2
	caculate_sounds = temp3

func _on_music_finished() -> void:
	if not Global.already_wrong:
		music.play()
