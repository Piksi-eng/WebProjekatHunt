
export class LoadoutView{

    constructor(currentGunVariantID, currentSecondGunID, currentTools, currentLoadout, currentLoadoutName, gunPrice, sGunPrice, toolsPrice, toolsCount, toolsSlots, toolsSlotsName, loadoutNames){


        this.currentGunVariantID = currentGunVariantID
        this.currentSecondGunID = currentSecondGunID
        this.currentTools = currentTools
        this.currentLoadout = currentLoadout
        this.currentLoadoutName = currentLoadoutName
        this.gunPrice = gunPrice
        this.sGunPrice = sGunPrice
        this.toolsPrice = toolsPrice
        this.toolsCount =toolsCount
        this.toolsSlots = toolsSlots
        this.toolsSlotsName = toolsSlotsName
        this.loadoutNames = loadoutNames


    }

    crtajLoadout(host){

        this.gunPrice=0
        this.sGunPrice=0
        this.toolsPrice=[]
        this.toolsCount=0
        this.toolsSlots=[false,false,false,false]
        this.toolsSlotsName=[]
        this.currentTools=[0,0,0,0,0]
        this.currentGunVariantID=0
        this.currentSecondGunID=0
        this.loadoutNames=[]
        this.currentLoadout = 0

        let l = document.createElement("div")
        l.className="loadoutForm"
        host.appendChild(l);
        
        let s = document.createElement("div")
        s.className="slot gunSlot"
        l.appendChild(s)

        let divudiv = document.createElement("div")
        divudiv.className="slot"
        l.appendChild(divudiv)

        s = document.createElement("div")
        s.className="sGunSlot"
        divudiv.appendChild(s)

        let tools = document.createElement("div")
        tools.className="tools"
        l.appendChild(tools)

        for(let i = 1; i < 5;i++)
        {
            let tool = document.createElement("div")
            tool.className="tool tool"+i+""
            let toolImg = document.createElement("div")
            toolImg.className="toolImg toolImg"+i+""
            tools.appendChild(tool)
            tool.appendChild(toolImg)
        }
        let allStats=document.createElement("div")
        allStats.className="allStats"
        l.appendChild(allStats)
        this.crtajStats(allStats,"MainGun");
        this.crtajStats(allStats,"SecondGun");

        let btnDiv = document.createElement("div")
        btnDiv.className="btnDiv"
        l.appendChild(btnDiv)
        let btn = document.createElement("button")
        btn.className="button buy"
        btn.innerText="BUY"
        btn.addEventListener("click", () => {
            
            let name = document.querySelector(".loadaoutName")

            if(name.value == ""){

                alert("Enter Loadout name!")
            }
            else if(this.currentGunVariantID == 0){
                alert("Pick a Gun!")
            }
            else if(this.currentSecondGunID == 0){
                alert("Pick a Secondery Gun!")
            }
            else if(this.currentTools[1] == 0){
                alert("Pick Tools!")
            } 
            else if(this.currentTools[2] == 0){
                alert("Pick Tools!")
            }
            else if(this.currentTools[3] == 0){
                alert("Pick Tools!")
            }
            else if(this.currentTools[4] == 0){
                alert("Pick Tools!")
            }
            else if(this.isNameValid(name.value))
            {
                alert("That name is alredy used !")

            }
            else{
                this.loadoutName=name.value
                this.loadoutNames.push(name.value)
                this.crtajLoadoutPicker(this.loadoutName)
                this.addLoadout()
            }
        })
        btnDiv.appendChild(btn)
        this.loadAllLoadouts()
    }

    loadAllLoadouts(){
        // let allPicks =document.querySelectorAll(".loadoutPick")
        // let loadoutPickerDiv = document.querySelector()

        fetch("https://localhost:5001/Loadout/GetLoadoutNames")
        .then(p=>{
            p.json().then(loadouts=>{
                loadouts.forEach(loadout => {
                    this.crtajLoadoutPicker(loadout)
                    this.loadoutNames.push(loadout)
                })
            })
        })
    }

    returnTrue(){
        return true
    }

    isNameValid(name){
        let result = this.loadoutNames.filter( l =>{
            return l == name

        } )

        if (result.length != 0)
        {
            return true
        }
        else {
            return false
        }

    }

    addLoadout(){
        let input = document.querySelector(".loadaoutName")

        fetch("https://localhost:5001/Loadout/AddLoadout/"+input.value+"/"+this.currentGunVariantID+"/"+this.currentSecondGunID+"/"+this.currentTools[1]+"/"
            +this.currentTools[2]+"/"+this.currentTools[3]+"/"+this.currentTools[4]+"/",
        {
            method:"POST"
        })
        .then(p=>{
                console.log("LoadoutSaved")
        })
    }

    crtajGunInfo(){
        
        let gunSlot = document.querySelector(".gunSlot")
        let selectedGun = document.querySelector(".gunSelect")
        let selectVariant = document.querySelector(".variantSelect")

        let gName = selectedGun.value
        let vName = selectVariant.value

        if(selectVariant.value == "")
        {
            vName = "No Variant"
        }

        
        //console.log(gName)
        //console.log(vName)

        let index;



        fetch("https://localhost:5001/GunsVariant/GetVariantStats/"+gName+"/"+vName+"/",
        {
            method: "GET"
        })
        .then(p=>{
            if(p.ok)
            {
                p.json().then(stats=>{                  

                    this.currentGunVariantID = stats[0].id
                    gunSlot.style.cssText = "background-image:  url(../Slike/Puske/"+stats[0].id+".webp), url(../Slike/slot_big_1.webp);"

                    let gunStats = []
                    gunStats.push(stats[0].totalDmg)
                    gunStats.push(stats[0].totalRange)
                    gunStats.push(stats[0].totalSpeed)
                    gunStats.push(stats[0].totalPrice)


                    this.crtajGunStats(gunStats)

                    })   
            }
    })
    
    }

    crtajGunStats(gunStats){

        const scale = (number, fromRange, toRange) => {
            return (
              ((number - fromRange[0]) * (toRange[1] - toRange[0])) /
                (fromRange[1] - fromRange[0]) +
              toRange[0]
            );
          };

          let dmgDisplay = scale(gunStats[0],[0,370],[0,100])
          let rangeDispaly = scale(gunStats[1],[0,350],[0,100])
          let speedDisplay = scale(gunStats[2],[0,700],[0,100])
          let priceDispaly = gunStats[3]

          let stat = document.querySelector("#MainGun.Damage")
          stat.style.width=dmgDisplay+"%"
          stat = document.querySelector("#MainGun.Range")
          stat.style.width=rangeDispaly+"%"
          stat = document.querySelector("#MainGun.Speed")
          stat.style.width=speedDisplay+"%"
          stat = document.querySelector(".buy")
          this.gunPrice=priceDispaly
        //   let price = this.gunPrice+this.sGunPrice+this.toolsPrice;
        //   stat.innerText="BUY"+" "+price+" $"
          this.updatePrice()
    }

    crtajSecondGunInfo(){
        let selcted=document.querySelector(".secondGunSelect")
        let sGunSlot = document.querySelector(".sGunSlot")
        //let divudiv = document.querySelector(".slot")


        fetch("https://localhost:5001/SecondGun/GetSecondGun/"+selcted.value+"/",
        {
            method: "GET"
        })
        .then(p=>{
            if(p.ok)
            {
                p.json().then(stats=>{  

                    //divudiv.style.cssText = "background-image:  url(../Slike/slot_big_1.webp);"
                    
                    this.currentSecondGunID = stats[0].sid
                    sGunSlot.style.cssText = "background-image:  url(../Slike/Pistolji/"+stats[0].sid+".webp);"
                    
                    console.log(stats[0])
                    let sGunSatats = []
                    sGunSatats.push(stats[0].sGunDmg)
                    sGunSatats.push(stats[0].sGunRange)
                    sGunSatats.push(stats[0].sGunSpeed)
                    sGunSatats.push(stats[0].sGunPrice)
                    this.crtajSecondGunStats(sGunSatats)

                    })   
            }
        })
    }

    crtajSecondGunStats(SecondStats){

        const scale = (number, fromRange, toRange) => {
            return (
              ((number - fromRange[0]) * (toRange[1] - toRange[0])) /
                (fromRange[1] - fromRange[0]) +
              toRange[0]
            );
          };


          let dmgDisplay = scale(SecondStats[0],[0,150],[0,100])
          let rangeDispaly = scale(SecondStats[1],[0,100],[0,100])
          let speedDisplay = scale(SecondStats[2],[0,500],[0,100])
          let priceDispaly = SecondStats[3]



          let stat = document.querySelector("#SecondGun.Damage")
          stat.style.width=dmgDisplay+"%"
          stat = document.querySelector("#SecondGun.Range")
          stat.style.width=rangeDispaly+"%"
          stat = document.querySelector("#SecondGun.Speed")
          stat.style.width=speedDisplay+"%"
          stat = document.querySelector(".buy")
          this.sGunPrice=priceDispaly
          let price = this.gunPrice+this.sGunPrice+this.toolsPrice;
          stat.innerText="BUY"+" "+price+" $"
          this.updatePrice()

    }

    updateTool(value){
        if(this.toolsCount == 4){
            let allCb=document.querySelectorAll("li input")
            allCb.forEach(el =>{
                el.disabled = false
            })
        }

        let str = value.replace(/\s/g, '')
        let cb=document.querySelector("#"+str)
        this.crtajToolImg(value)

        if(cb.checked)
        {  
            this.toolsCount++
        }
        else
        {
            this.toolsCount--
        }
        if(this.toolsCount == 4)
        {
            let allCb=document.querySelectorAll("li input")
            allCb.forEach(el =>{
                if(!el.checked)
                el.disabled = true
            })
        }
    }

    crtajToolImg(value){
        let str = value.replace(/\s/g, '')
        let cb=document.querySelector("#"+str)
        //console.log(cb.checked)
        if(cb.checked)
        {
            for(let i = 1;i < 5; i++)
            {   
                
                //console.log(i)
                if(!this.toolsSlots[i])
                {
                    this.fetchTools(i, value)
                    break
                }
            }
        }
        else
        {
            for(let i = 1; i < 5; i++)
            {
                if(this.toolsSlotsName[i]==value)
                {
                    
                    this.toolsSlotsName[i]==""
                    let toolImg = document.querySelector(".toolImg"+i)
                    toolImg.style.background = 'none';
                    this.toolsSlots[i]=false
                    this.toolsPrice[i]=0
                    this.currentTools[i]=0
                    this.updatePrice()
                }
            }
        }
    }

    async fetchTools(i, value){
        const response = await fetch("https://localhost:5001/Tools/GetTool/"+value+"/",
        {
            method: "GET"
        });
        const toolInfo = await response.json();
        let toolImg = document.querySelector(".toolImg"+i)
        toolImg.addEventListener("mouseenter", () =>{
        })

        toolImg.style.cssText = "background-image:  url(../Slike/Tools/"+toolInfo[0].tid+".webp);"
        this.currentTools[i] = toolInfo[0].tid
        this.toolsSlots[i]=true
        this.toolsSlotsName[i]=value
        this.toolsPrice[i] = toolInfo[0].tPrice
        this.updatePrice()
    }
        

    crtajLoadoutPicker(name){

        let loadoutPicker = document.querySelector(".loadoutPicker")
        let newLoadout = document.createElement("div")
        newLoadout.className="loadoutPick"
        let id = name.replace(/\s/g, '')
        newLoadout.setAttribute("id",id)
        newLoadout.addEventListener("click", () => {

            let allPicks = document.querySelectorAll(".loadoutPick")
            allPicks.forEach(p =>{              
                p.style.backgroundColor = ""
            })
            this.crtajLoadoutFull(newLoadout.innerText)
           newLoadout.style.backgroundColor = "#101011";

        })
        newLoadout.innerText=name
        loadoutPicker.appendChild(newLoadout)

        //dodaj u bazu

    }

    crtajLoadoutFull(value){
        ///Loadout/GetLoadout/{Name}
        fetch("https://localhost:5001/Loadout/GetLoadout/"+value+"/",
        {
            method: "GET"
        })
        .then(p=>{
            if(p.ok)
            {
                p.json().then(loadoutStats=>{                  

                    if (this.currentLoadout == 0){
                        this.crtajEditDelete()
                    }
                    
                    console.log(loadoutStats[0])
                    let allCb = document.querySelectorAll("li input")
                    allCb.forEach(el =>{
                        el.disabled = false
                        el.checked = false
                    })

                    let gunSlot = document.querySelector(".gunSlot")
                    let sGunSlot = document.querySelector(".sGunSlot")

                    let mainStats = []
                    mainStats.push(loadoutStats[0].totalMainDmg)
                    mainStats.push(loadoutStats[0].totalMainRange)
                    mainStats.push(loadoutStats[0].totalMainSpeed)
                    mainStats.push(loadoutStats[0].totalVariantPrice)
                    let secondStats = []
                    secondStats.push(loadoutStats[0].secondGunDmg)
                    secondStats.push(loadoutStats[0].secondGunRange)
                    secondStats.push(loadoutStats[0].secondGunSpeed)
                    secondStats.push(loadoutStats[0].secondPrice)

                    let tool1 = loadoutStats[0].tool1Name
                    let tool2 = loadoutStats[0].tool2Name
                    let tool3 = loadoutStats[0].tool3Name
                    let tool4 = loadoutStats[0].tool4Name

                    

                    let str = tool1.replace(/\s/g, '')
                    document.getElementById(str).checked = true;

                    str = tool2.replace(/\s/g, '')
                    document.getElementById(str).checked = true;

                    str = tool3.replace(/\s/g, '')
                    document.getElementById(str).checked = true;
                 

                    str = tool4.replace(/\s/g, '')
                    document.getElementById(str).checked = true; 

                    allCb=document.querySelectorAll("li input")
                    allCb.forEach(el =>{
                        if(!el.checked)
                        el.disabled = true
                    })

                    

                    let toolImg1 = document.querySelector(".toolImg1")
                    let toolImg2 = document.querySelector(".toolImg2")
                    let toolImg3 = document.querySelector(".toolImg3")
                    let toolImg4 = document.querySelector(".toolImg4")

                    toolImg1.style.cssText = "background-image:  url(../Slike/Tools/"+loadoutStats[0].tool1ID+".webp);"
                    toolImg2.style.cssText = "background-image:  url(../Slike/Tools/"+loadoutStats[0].tool2ID+".webp);"
                    toolImg3.style.cssText = "background-image:  url(../Slike/Tools/"+loadoutStats[0].tool3ID+".webp);"
                    toolImg4.style.cssText = "background-image:  url(../Slike/Tools/"+loadoutStats[0].tool4ID+".webp);"

                    this.currentTools[1]=loadoutStats[0].tool1ID
                    this.currentTools[2]=loadoutStats[0].tool2ID
                    this.currentTools[3]=loadoutStats[0].tool3ID
                    this.currentTools[4]=loadoutStats[0].tool4ID

                    this.toolsSlots[1]=true
                    this.toolsSlots[2]=true
                    this.toolsSlots[3]=true
                    this.toolsSlots[4]=true



                    this.toolsSlotsName[1]=loadoutStats[0].tool1Name
                    this.toolsSlotsName[2]=loadoutStats[0].tool2Name
                    this.toolsSlotsName[3]=loadoutStats[0].tool3Name
                    this.toolsSlotsName[4]=loadoutStats[0].tool4Name
                    this.toolsCount = 4;

                    
                    this.currentGunVariantID = loadoutStats[0].variantID
                    this.currentSecondGunID = loadoutStats[0].secondID
                    gunSlot.style.cssText = "background-image:  url(../Slike/Puske/"+loadoutStats[0].variantID+".webp), url(../Slike/slot_big_1.webp);"
                    sGunSlot.style.cssText = "background-image:  url(../Slike/Pistolji/"+loadoutStats[0].secondID+".webp);"

                    this.crtajSecondGunStats(secondStats)
                    this.crtajGunStats(mainStats)

                    let input = document.querySelector(".loadaoutName")
                    input.value = loadoutStats[0].lName
                    this.currentLoadout = loadoutStats[0].lid
                    this.currentLoadoutName = loadoutStats[0].lName
                     console.log(this.currentLoadoutName)
                    })   
            }
    })
    }

    crtajEditDelete(){
        let btnDiv = document.querySelector(".btnDiv")
        let btn = document.createElement("button")
        btn.className="button Delete"
        btn.innerText="Delete"
        btn.addEventListener("click", () =>{
            console.log(this.currentLoadoutName)
            this.deleteCurrentLoadout()
        })
        btnDiv.appendChild(btn)

        btn = document.createElement("button")
        btn.className="button Edit"
        btn.innerText="Edit"
        btn.addEventListener("click", () =>{
            console.log(this.currentLoadoutName)
            this.editCurrentLadout()
        })
        btnDiv.appendChild(btn)

    }
    editCurrentLadout(){

         let input = document.querySelector(".loadaoutName")
         if(!this.isNameValid(input.value))
         {
            fetch("https://localhost:5001/Loadout/EditLoadout/"+this.currentLoadoutName+"/"+input.value+"/"+this.currentGunVariantID+"/"+this.currentSecondGunID+"/"+this.currentTools[1]+"/"
            +this.currentTools[2]+"/"+this.currentTools[3]+"/"+this.currentTools[4]+"/",
            {
                method:"PUT"
            })
            .then(p=>{
    
                    let id = this.currentLoadoutName.replace(/\s/g, '')
                    let pick = document.querySelector("#"+id)
                    pick.innerText=input.value
                    console.log("Loadout Edited!")
                    this.loadoutNames = this.loadoutNames.filter((value)=>{
    
                        return value != this.currentLoadoutName
                   
                })     
                this.loadoutNames.push(input.value)
            }) 
         }
         else{
             alert("Name is already in use!")
         }
    }
    deleteCurrentLoadout(){

            fetch("https://localhost:5001/Loadout/DeleteLoadout/"+this.currentLoadout+"/",
        {
            method:"DELETE"
        })
        .then(p=>{
                let id = this.currentLoadoutName.replace(/\s/g, '')
                let pick = document.querySelector("#"+id)
                let picker = document.querySelector(".loadoutPicker")
                picker.removeChild(pick)
                console.log("Loadout Deleted!")
                this.loadoutNames = this.loadoutNames.filter((value)=>{

                            return value != this.currentLoadoutName
                       
                })
                console.log(this.currentLoadoutName)
        })

    }

    crtajStats(host,ime){

        let statsArray= ["Damage", "Range" , "Speed"]
        let stats=document.createElement("div")
        stats.className="stats"
        host.appendChild(stats)

        let title=document.createElement("h1")
        title.innerText=ime
        stats.appendChild(title)

        //let li=document.createElement("li")

        statsArray.forEach(el => {

            let li=document.createElement("li")        
            let h3=document.createElement("h3")

            h3.innerText=el

            li.appendChild(h3)  
            
            let spanBar=document.createElement("span") 
            spanBar.className="bar"
            li.appendChild(spanBar)
            let spanEl=document.createElement("span") 
            spanEl.className=el
            spanEl.setAttribute("id",ime)
            spanBar.appendChild(spanEl)


            stats.appendChild(li)
        });


    }
    updatePrice(){
        let btn = document.querySelector(".buy")
        var tPrice=0
        this.toolsPrice.forEach(p => {
            tPrice+=p
        })
        let price = this.gunPrice+this.sGunPrice+tPrice;
        btn.innerText="BUY"+" "+price+" $"
    }

    
}