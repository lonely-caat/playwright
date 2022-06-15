a = [3,2,4,6,9,33]

function bubbleButt(list){
    for (let element in list){
        for (let el of list){
            if (list[el] > list[el+1]){
                // let tmp = list[el]
                // list[el] = list[el+1]
                // list[el+1] = tmp
                [list[el], list[el+1] = list[el+1], list[el]]
            }
        }
    }
    return list
}

console.log(bubbleButt(a))