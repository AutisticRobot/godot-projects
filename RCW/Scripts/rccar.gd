extends VehicleBody3D

func _physics_process(delta):
    steering = lerp(steering, Input.get_axis("rit","lft") * 0.4, 5 * delta)
    engine_force = Input.get_axis("dwn","up") * 100