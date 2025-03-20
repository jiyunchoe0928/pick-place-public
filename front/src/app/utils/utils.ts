export const convertKeyToName = (key: string) => {
  return key.replace(/[*_+]/g, " ").replace(/^\S+\s*/, "");
};

export const formatHours = (input: string) => {  
  const [prefix, hours] = input.split(": ");  
  const formattedHours = hours.split(", ").join("\n");  

  return `${prefix}:\n${formattedHours}`;  
}  