module UpdateList

let updateList (l:int list) =
    List.map (fun element -> System.Math.Abs(element:int)) l