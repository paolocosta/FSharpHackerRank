module ListFlatten

open NUnit.Framework
open FsUnit

let flattenList l =
    let rec helper l = 
     match l with
        |[] -> []
        |head::tail -> 
            match head with
            | [] as l1 -> helper [l1] @ helper tail
            | _::_ as l2 -> l2::helper tail
            | _ as l3 -> l3::helper tail
    helper [l]     

