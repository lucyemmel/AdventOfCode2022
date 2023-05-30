from typing import Set, List, Tuple

Position = List[int]

def assert_positive_positions(positions: List[Position]) -> None:
    assert all(elem >= 0 for position in positions for elem in position), f'Invalid positions'


def is_diagonally_adjacent(head_position: Position, tail_position: Position) -> bool:
    return abs(head_position[0] - tail_position[0]) == 1 and abs(head_position[1] - tail_position[1]) == 1


def update_tail_position_if_necessary(head_position: Position, tail_position: Position) -> None:
    # overlapping position needs no update
    if head_position == tail_position:
        return
    # check if tail and head in the same row
    if head_position[0] == tail_position[0]:
        if head_position[1] > tail_position[1] + 1:
            tail_position[1] += 1
        elif head_position[1] < tail_position[1] - 1:
            tail_position[1] -= 1
    # check if tail and head in same column
    elif head_position[1] == tail_position[1]:
        if head_position[0] > tail_position[0] + 1:
            tail_position[0] += 1
        elif head_position[0] < tail_position[0] - 1:
            tail_position[0] -= 1
    elif not is_diagonally_adjacent(head_position, tail_position):
        if head_position[0] > tail_position[0]:
            tail_position[0] += 1
        else:
            tail_position[0] -= 1
        if head_position[1] > tail_position[1]:
            tail_position[1] += 1
        else:
            tail_position[1] -= 1


def update_positions_from_instruction(instr: str, head_position: Position, tail_position: Position,
                                      visited_positions: Set[Tuple[int, int]]) -> None:
    direction, steps = instr.split(' ')
    steps = int(steps)

    match direction:
        case 'U':
            for i in range(steps):
                head_position[1] += 1
                update_tail_position_if_necessary(head_position, tail_position)
                visited_positions.add((tail_position[0], tail_position[1]))
        case 'D':
            for i in range(steps):
                head_position[1] -= 1
                update_tail_position_if_necessary(head_position, tail_position)
                visited_positions.add((tail_position[0], tail_position[1]))
        case 'L':
            for i in range(steps):
                head_position[0] -= 1
                update_tail_position_if_necessary(head_position, tail_position)
                visited_positions.add((tail_position[0], tail_position[1]))
        case 'R':
            for i in range(steps):
                head_position[0] += 1
                update_tail_position_if_necessary(head_position, tail_position)
                visited_positions.add((tail_position[0], tail_position[1]))
        case _:
            raise RuntimeError(f'Invalid direction: {direction}')
    assert_positive_positions([head_position, tail_position])


def update_other_positions_if_necessary_ten_knots(positions: List[Position]) -> None:
    # compare each position with its tail -> previous element
    for index, position in enumerate(positions):
        if index == 0:
            continue
        head_position = positions[index - 1]
        tail_position = position
        # overlapping position needs no update
        if head_position == tail_position:
            continue
        # check if tail and head in the same row
        if head_position[0] == tail_position[0]:
            if head_position[1] > tail_position[1] + 1:
                tail_position[1] += 1
            elif head_position[1] < tail_position[1] - 1:
                tail_position[1] -= 1
        # check if tail and head in same column
        elif head_position[1] == tail_position[1]:
            if head_position[0] > tail_position[0] + 1:
                tail_position[0] += 1
            elif head_position[0] < tail_position[0] - 1:
                tail_position[0] -= 1
        elif not is_diagonally_adjacent(head_position, tail_position):
            if head_position[0] > tail_position[0]:
                tail_position[0] += 1
            else:
                tail_position[0] -= 1
            if head_position[1] > tail_position[1]:
                tail_position[1] += 1
            else:
                tail_position[1] -= 1


def update_positions_from_instruction_ten_knots(instr: str, positions: list[Position],
                                                visited_positions: Set[Tuple[int, int]]) -> None:
    direction, steps = instr.split(' ')
    steps = int(steps)
    head_position = positions[0]

    match direction:
        case 'U':
            for i in range(steps):
                head_position[1] += 1
                update_other_positions_if_necessary_ten_knots(positions)
                visited_positions.add((positions[-1][0], positions[-1][1]))
        case 'D':
            for i in range(steps):
                head_position[1] -= 1
                update_other_positions_if_necessary_ten_knots(positions)
                visited_positions.add((positions[-1][0], positions[-1][1]))
        case 'L':
            for i in range(steps):
                head_position[0] -= 1
                update_other_positions_if_necessary_ten_knots(positions)
                visited_positions.add((positions[-1][0], positions[-1][1]))
        case 'R':
            for i in range(steps):
                head_position[0] += 1
                update_other_positions_if_necessary_ten_knots(positions)
                visited_positions.add((positions[-1][0], positions[-1][1]))
        case _:
            raise RuntimeError(f'Invalid direction: {direction}')
    assert_positive_positions(positions)


def solution_part_one():
    # high positive start values to ensure that positions never become negative to make checks easier
    head_position = [300, 300]
    tail_position = [300, 300]
    visited_positions = {(300, 300)}

    with open('input.txt') as file:
        for line in file:
            update_positions_from_instruction(line, head_position, tail_position, visited_positions)
    print(len(visited_positions))


def solution_part_two():
    positions = [[300, 300], [300, 300], [300, 300], [300, 300], [300, 300], [300, 300], [300, 300], [300, 300],
                 [300, 300], [300, 300]]
    visited_positions = {(300, 300)}
    with open('input.txt') as file:
        for line in file:
            update_positions_from_instruction_ten_knots(line, positions, visited_positions)
    print(len(visited_positions))


if __name__ == '__main__':
    solution_part_two()
