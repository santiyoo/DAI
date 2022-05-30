import fs from 'fs'

export const logErr = (newcontent) => {
    console.log("newcontent", newcontent)
    fs.writeFileSync("./error.txt", JSON.stringify(newcontent));
}