def solution_part_one():
    current_cycle = 1
    register_value = 1
    result = 0
    cycles_to_sum = [20, 60, 100, 140, 180, 220]

    with open('input.txt') as file:
        for line in file:
            if current_cycle in cycles_to_sum:
                result += current_cycle * register_value
            if line.startswith('noop'):
                current_cycle += 1
            else:
                if current_cycle + 1 in cycles_to_sum:
                    result += (current_cycle + 1) * register_value
                register_value += int(line.split(' ')[1])
                current_cycle += 2
    print(result)


def add_pixel(image: str, current_cycle: int, register_value: int) -> str:
    current_pixel = (current_cycle - 1) % 40
    if register_value in range(current_pixel - 1, current_pixel + 2):
        image += '#'
    else:
        image += '.'
    if current_cycle % 40 == 0:
        image += '\n'
    return image


def solution_part_two():
    current_cycle = 1
    register_value = 1
    image = ""

    with open('input.txt') as file:
        for line in file:
            if line.startswith('noop'):
                image = add_pixel(image, current_cycle, register_value)
                current_cycle += 1
            else:
                image = add_pixel(image, current_cycle, register_value)
                current_cycle += 1
                image = add_pixel(image, current_cycle, register_value)
                current_cycle += 1
                register_value += int(line.split(' ')[1])
    print(image)


if __name__ == '__main__':
    solution_part_two()
